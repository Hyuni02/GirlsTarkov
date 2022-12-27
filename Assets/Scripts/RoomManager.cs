using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public GameObject[] SpawnPoints;

    [HideInInspector]
    public Transform pos;
    int index;

    private void Start() {
        if (instance) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

    }

    public override void OnEnable() {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable() {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        if(scene.buildIndex== 2) {
            CursorLock(true);

            SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            index = (int)Random.Range(0, SpawnPoints.Length);
            pos = SpawnPoints[index].transform;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public void Escape(GameObject _player) {
        Debug.Log("Escape : " + PhotonNetwork.NickName);
        Debug.Log("Escape : " + _player.name);
        PhotonNetwork.Disconnect();

        Invoke("ToLobby", 2f);
    }

    public void Die(GameObject _player) {
        Debug.Log("Die : " + _player.name);
        Debug.Log("Die : " + PhotonNetwork.NickName);
        PhotonNetwork.Disconnect();

        Invoke("ToLobby", 2f);
    }

    void ToLobby() {
        CursorLock(false);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void CursorLock(bool state) {
        if (state) {
            Cursor.visible= false;
            Cursor.lockState= CursorLockMode.Locked;
        }
        else {
            Cursor.visible= true;
            Cursor.lockState= CursorLockMode.None;
        }
    }
}
