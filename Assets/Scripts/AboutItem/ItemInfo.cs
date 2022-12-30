using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemInfo : MonoBehaviour
{
    public enum Type { item, bag, gun, mag, helmet, armor, ammo}
    public Type type;
    public string ItemName;
    public int ItemCode;
    public string ItemDescription;
    public float size;
    public float weight;

    protected bool picked;

    [HideInInspector]
    BoxCollider col;
    MeshRenderer rend;
    Rigidbody rb;

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
        Destroy(rb);

        transform.SetParent(parent);
        Debug.Log("Pick" + ItemName);
        parent.GetComponent<ItemContainerInfo>()?.UpdateContainerState();

        picked = true;
    }
    //바닥으로 아이템 버리기
    public virtual void Thrown() {
        col = GetComponent<BoxCollider>();
        rend = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        col.enabled = true;
        rend.enabled = true;
        gameObject.AddComponent<Rigidbody>();

        picked = false;
        transform.SetParent(null);

        Debug.Log(ItemName + " has Thrown");
    }
    //공간에서 공간으로 아이템 이동하기
    public virtual void MoveItem(Transform from, Transform to) {
        transform.SetParent(to);
        from.GetComponent<ItemContainerInfo>()?.UpdateContainerState();
        to.GetComponent<ItemContainerInfo>()?.UpdateContainerState();
    }
}
