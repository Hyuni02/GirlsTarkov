using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.SceneManagement;

public class ItemInfo : MonoBehaviour
{
    public enum Type { item, bag, gun, mag, helmet, armor, ammo, box, body}
    public Type type;
    public string ItemName;
    public int ItemCode;
    public string ItemDescription;
    public Sprite ItemIcon;
    public float size;
    public float weight;

    protected bool picked;
    [SerializeField]
    protected bool equiped;

    [HideInInspector]
    protected BoxCollider col;
    protected MeshRenderer rend;
    protected Rigidbody rb;
    PhotonView pv;

    public virtual void Interact(GameObject player) {
        Inventory playerInventory = player.GetComponent<Inventory>();
        if (playerInventory.bag != null) {
            if (playerInventory.bag.currentSize + size <= playerInventory.bag.MaxSize) {
                Pick(playerInventory.bag.transform);
            }
            else {
                Debug.Log("Can't Pick " + ItemName);
            }
        }
        else {
            Debug.Log("Can't Pick " + ItemName);
        }
    }

    //바닥에서 아이템 줍기
    public virtual void Pick(Transform parent) {
        col = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        col.enabled = false;
        rend.enabled = false;
        rb.useGravity= false;

        transform.SetParent(parent);
        Debug.Log("Pick " + ItemName);
        parent.GetComponent<ItemContainerInfo>()?.UpdateContainerState();

        picked = true;

        //네트워크 부분
        pv = transform.root.GetComponent<PhotonView>();
        if (pv == null) {
            Debug.Log("Cant Find Parent's PV");
            return;
        }
        else {
            if (!pv.IsMine)
                pv.RPC("RPC_Pick", RpcTarget.All, GetComponent<PhotonView>().ViewID, parent.GetComponent<PhotonView>().ViewID);
        }
    }

    //바닥으로 아이템 버리기
    public virtual void Thrown() {
        //pv = transform.root.GetComponent<PhotonView>();
        pv = RoomManager.instance.LocalPlayer.GetComponent<PhotonView>();

        col = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        col.enabled = true;
        rend.enabled = true;
        rb.useGravity = true;
        rb.velocity= Vector3.zero;

        picked = false;
        equiped = false;
        Transform p = transform.parent;
        transform.SetParent(null);

        p.GetComponent<ItemContainerInfo>()?.UpdateContainerState();
        UI_Inventory.instance.UpdateAll();
        if(p.parent!= null) {
            UI_Inventory.instance.UpdateLoot(p.parent.GetComponent<Inventory>());

        }
        Debug.Log(ItemName + " has Thrown");
        
        transform.position = new Vector3(
            p.position.x,
            p.position.y + 2,
            p.position.z);

        //네트워크 부분

        //Debug.Log(pv.name);
        if (pv == null) {
            Debug.Log("Cant Find Parent's PV");
            return;
        }
        else {
            if(!pv.IsMine)
                pv.RPC("RPC_Thrown", RpcTarget.All, GetComponent<PhotonView>().ViewID);
        }
    }
    //공간에서 공간으로 아이템 이동하기
    public virtual void MoveItem(Transform from, Transform to) {
        transform.SetParent(to);
        from.GetComponent<ItemContainerInfo>()?.UpdateContainerState();
        to.GetComponent<ItemContainerInfo>()?.UpdateContainerState();
        print(name + " : " + from.name + " -> " + to.name);
    }
}
