using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    [SerializeField] GameObject Panel_Loading;
    [SerializeField] GameObject[] Panels;
    [SerializeField] GameObject[] Image_NPC;
    [SerializeField] TMP_Text Text_RoomCode;
    [SerializeField] Transform RoomListContainer;
    [SerializeField] GameObject RoomItem;
    [SerializeField] Transform PlayerListContainer;
    [SerializeField] GameObject PlayerItem;
    [SerializeField] GameObject Button_Start;

    [HideInInspector] public int side;

    private void Awake() {
        Instance = this; 
    }

    void Start() {
        Panel_Loading.SetActive(true);

        if (!PlayerPrefs.HasKey("UserName")) {
            SceneManager.LoadScene(1);
            return;
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to Maseter");
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby() {
        Debug.Log("Joined Lobby");
        Debug.Log("UserName : " + PlayerPrefs.GetString("UserName"));
        Debug.Log("Side : " + PlayerPrefs.GetInt("Side"));

        side = PlayerPrefs.GetInt("Side");
        PhotonNetwork.NickName = PlayerPrefs.GetString("UserName");
        CloseAllPanel();
        Open_Panel_Lobby();
        Panel_Loading.SetActive(false);
    }
    public override void OnJoinedRoom() {
        Debug.Log("Join Room." + PhotonNetwork.CurrentRoom.Name);
        
        Text_RoomCode.text = "Room." + PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform child in PlayerListContainer) {
            Destroy(child.gameObject);
        }
        foreach (Player p in players) {
            Instantiate(PlayerItem, PlayerListContainer).GetComponent<PlayerItem>().SetUp(p);
        }

        Button_Start.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnLeftRoom() {
        Panels[2].SetActive(false);
        Button_Start.SetActive(false) ;
        Text_RoomCode.text = "Connecting...";
    }
    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.LogAssertion(returnCode + " : " + message);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        foreach(Transform t in RoomListContainer) {
            Destroy(t.gameObject);
        }
        foreach(RoomInfo room in roomList) {
            if (room.RemovedFromList)
                continue;
            Instantiate(RoomItem, RoomListContainer).GetComponent<RoomItem>().SetUp(room);
        }
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Debug.Log(newPlayer.NickName + "Joined Room");
        Instantiate(PlayerItem, PlayerListContainer).GetComponent<PlayerItem>().SetUp(newPlayer);
    }
    public override void OnMasterClientSwitched(Player newMasterClient) {
        Button_Start.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void FindGame() {
        Panels[2].SetActive(true);
    }
    public void CreateRoom() {
        PhotonNetwork.CreateRoom(((int)Random.Range(1000,10000)).ToString());
        Panels[3].SetActive(true);
    }
    public void CloseRoom() {
        Debug.Log("Leave Room." + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LeaveRoom();
    }
    public void JoinRoom(RoomInfo info) {
        PhotonNetwork.JoinRoom(info.Name);
        Panels[3].SetActive(true);

    }
    public void GameStart() {
        PhotonNetwork.LoadLevel(2);
    }

    public void CloseAllPanel() {
        foreach (GameObject panel in Panels) {
            panel.SetActive(false);
        }
    }
    void CloseAllNPC() {
        foreach (GameObject npc in Image_NPC) {
            npc.SetActive(false);
        }
    }
    public void Open_Panel_Lobby() {
        //Open Panel
        Panels[0].SetActive(true);
        //Show Side QuestNPC
        CloseAllNPC();
        switch (side) {
            case 0://카리나
                Image_NPC[3].SetActive(true);
                break;
            case 1://제레
                Image_NPC[0].SetActive(true);
                break;
            case 2://안젤리나
                Image_NPC[4].SetActive(true);
                break;
        }
    }
    public void Open_Panel_Cafe() {
        //Open Panel
        Panels[1].SetActive(true);
        //Show Side QuestNPC

    }
    public void Open_Panel_Pub() {
        //Open Panel
        Panels[2].SetActive(true);
        //Show Side QuestNPC

    }
    public void Open_Panel_Restore() {
        //Open Panel
        Panels[1].SetActive(true);
        //Show Side RestoreNPC
        CloseAllNPC();
        switch (side) {
            case 0://페르시카
                Image_NPC[1].SetActive(true);
                break;
            case 1://데레
                Image_NPC[2].SetActive(true);
                break;
            case 2://안젤리아
                Image_NPC[4].SetActive(true);
                break;
        }
    }
}
