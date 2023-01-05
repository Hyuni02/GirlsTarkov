using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Sprite PlayerIcon;

    public ArmorInfo Helmet;
    public ArmorInfo Armor;
    public Gun main_Weapon;
    //public GameObject sub_Weapon;

    //public GameObject vest;
    public ItemContainerInfo bag;

    private void Start() {
        //main_Weapon = GetComponentInChildren<Gun>().gameObject;
        //vest = GameObject.Find("Vest1");
        //bag = GameObject.Find("Bag1").GetComponent<ItemContainerInfo>();
    }


    [PunRPC]
    void RPC_Pick(int _itemviewid, int _ownerviewid) {
        ItemInfo target = PhotonView.Find(_itemviewid).GetComponent<ItemInfo>();
        target.transform.parent = PhotonView.Find(_ownerviewid).transform;

        BoxCollider col = target.GetComponent<BoxCollider>();
        MeshRenderer rend = target.GetComponent<MeshRenderer>();
        Rigidbody rb = target.GetComponent<Rigidbody>();

        col.enabled = false;
        rend.enabled = false;
        rb.useGravity= false;

        //target.gameObject.SetActive(false);
    }
    [PunRPC]
    void RPC_Thrown(int _itemviewid) {
        ItemInfo target = PhotonView.Find(_itemviewid).GetComponent<ItemInfo>();
        target.transform.parent = null;

        BoxCollider col = target.GetComponent<BoxCollider>();
        MeshRenderer rend = target.GetComponent<MeshRenderer>();
        Rigidbody rb = target.GetComponent<Rigidbody>();

        col.enabled = true;
        rend.enabled = true;
        rb.useGravity = true;

        //target.gameObject.SetActive(true);
    }
}
