using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    string character;

    private void Awake() {
        pv = GetComponent<PhotonView>();
    }

    private void Start() {
        if (pv.IsMine) {
            CreateController();
        }
    }

    void CreateController() {
        Debug.Log("instantiated PlayerContorller");
        switch (PlayerPrefs.GetInt("Side")) {
            case 0: character = "Nikcy"; break;
            case 1: character = "LiDailin"; break; 
            case 2: character = "Hyunwoo"; break;
        }
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", character), Vector3.zero, Quaternion.identity);
        FindObjectOfType<CinemachineVirtualCamera>().Follow = player.transform.GetChild(0).transform;
    }
}