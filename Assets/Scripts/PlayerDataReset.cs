using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerDataReset : MonoBehaviour
{
    Input _input;

    private void Awake() {
        _input = new Input();
        _input.ResetData.reset.performed += val => PlayerPrefs.DeleteAll();
        _input.ResetData.reset.performed += val => Debug.Log("Delete UserData");
    }

    public void F_ResetData() {
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete UserData");
        SceneManager.LoadScene(1);
    }

    private void OnEnable() {
        _input.Enable();
    }
    private void OnDisable() {
        _input.Disable();
    }
}
