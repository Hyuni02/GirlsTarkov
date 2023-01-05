using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : ItemContainerInfo {
    [SerializeField]
    Inventory inventory;

    public override void Start() {
        base.Start();
    }

    public override void Interact(GameObject player) {
        player.GetComponent<InventoryViewer>().OpenInventory();
        UI_Inventory.instance.UpdateLoot(inventory);
    }
}
