using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInitialization : MonoBehaviour {
    [SerializeField] GameObject Panel_SetUserName;
    [SerializeField] TMP_InputField userNameInputField;
    public enum Side { AR , _404, DEFY }
    Side side;

    public void OnSelectSide(int _index) {
        side = (Side)_index;
        Panel_SetUserName.SetActive(true);
    }

    public void OnBack() {
        Panel_SetUserName.SetActive(false);
    }
    public void OnEnterUserName() {
        CheckUserName();
        SetUserData();
        SceneManager.LoadScene(0);
    }
    void CheckUserName() {
        if (string.IsNullOrEmpty(userNameInputField.text)) { return; }
    }
    struct UserData { public string userName; public int side; }
    void SetUserData() {
        PlayerPrefs.SetString("UserName", userNameInputField.text);
        PlayerPrefs.SetInt("Side", (int)side);
    }
}
