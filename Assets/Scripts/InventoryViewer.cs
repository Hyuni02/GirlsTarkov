using Photon.Pun;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewer : MonoBehaviour
{
    public bool open;
    Inventory inventory;
    PhotonView photonView;
    GameObject uI_Inventory;

    private void Start() {
        inventory = GetComponent<Inventory>();
        photonView = GetComponent<PhotonView>();
        //if (photonView.IsMine)
        uI_Inventory = GameObject.Find("UI_Inventory");
        if (uI_Inventory != null)
            uI_Inventory.SetActive(false);
    }

    public void OpenInventory() {
        uI_Inventory.GetComponent<UI_Inventory>().UpdateAll();
        uI_Inventory.GetComponent<UI_Inventory>().UpdateLoot();
        open= true;
        uI_Inventory.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseInventory() {
        open= false;
        uI_Inventory.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
