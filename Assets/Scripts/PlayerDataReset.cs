using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerDataReset : MonoBehaviour
{
    Input _input;

    private void Awake() {
        _input = new Input();
        _input.ResetData.reset.performed += _ => F_ResetData();
    }

    public void F_ResetData() {
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete UserData");
        SceneManager.LoadScene(1);
        Destroy(GameObject.FindObjectOfType<RoomManager>().gameObject);
        PhotonNetwork.Disconnect();
    }

    private void OnEnable() {
        _input.Enable();
    }
    private void OnDisable() {
        _input.Disable();
    }
}
