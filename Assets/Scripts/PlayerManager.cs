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
    GameObject player;

    private void Awake() {
        pv = GetComponent<PhotonView>();

        if (pv.IsMine) {
            CreateController();
        }
    }

    private void Start() {

    }

    void CreateController() {
        Debug.Log("instantiated PlayerContorller");
        switch (PlayerPrefs.GetInt("Side")) {
            case 0: character = "Nicky"; break;
            case 1: character = "LiDailin"; break; 
            case 2: character = "Hyunwoo"; break;
        }
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", character), RoomManager.instance.pos.position, RoomManager.instance.pos.rotation, 0, new object[] { pv.ViewID });
        FindObjectOfType<CinemachineVirtualCamera>().Follow = player.transform.GetChild(0).transform;
        RoomManager.instance.LocalPlayer = player;
    }

    public void Die() {
        //PhotonView.Destroy(player);
        RoomManager.instance.Die(player);
    }
}
