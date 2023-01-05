using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInfo : ItemInfo
{
    public float MaxSize;
    public float currentSize;
    public float selfWeight;
    public float totalWeight;

    public virtual void Start() {
        UpdateContainerState();
    }

    public override void Interact(GameObject player) {
        Inventory playerInventory = player.GetComponent<Inventory>();
        if(playerInventory.bag == null) {
            playerInventory.bag = this;
            equiped = true;
            Pick(player.transform);
            return;
        }

        base.Interact(player);
    }

    public virtual void UpdateContainerState() {
        currentSize = 0;
        totalWeight = 0;
        for(int i=0;i<transform.childCount; i++) {
            currentSize += transform.GetChild(i).GetComponent<ItemInfo>().size;
            totalWeight += transform.GetChild(i).GetComponent<ItemInfo>().weight;
        }
        totalWeight += selfWeight;
        Debug.Log("UpdateState " + ItemName);
    }

    public override void Thrown() {
        if(equiped) {
            transform.parent.GetComponent<Inventory>().bag = null;  
        }
        base.Thrown();
    }
}
