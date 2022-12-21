using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    RoomInfo roomInfo;

    public void SetUp(RoomInfo info) {
        roomInfo= info;
        text.text = "Room." + info.Name;
    }

    public void OnClick() {
        GameManager.Instance.JoinRoom(roomInfo);
    }
}
