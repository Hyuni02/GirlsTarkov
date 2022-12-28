using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultViewer : MonoBehaviour
{
    [SerializeField] GameObject Panel_Escape;    
    [SerializeField] GameObject Panel_Died;
    [SerializeField] GameObject Button_ToLobby;

    private void Start() {
        Panel_Escape.SetActive(false);
        Panel_Died.SetActive(false);
        Button_ToLobby.GetComponent<Button>().onClick.AddListener(ToLobby);

        //데이터 저장

        //정보 불러오기

        if (RoomManager.instance.escape) {
            Panel_Escape.SetActive(true);
        }
        else {
            Panel_Died.SetActive(true);
        }
    }

    void ToLobby() {
        RoomManager.instance.ToLobby();
    }
}
