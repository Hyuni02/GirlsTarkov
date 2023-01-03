using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInfo : ItemInfo
{
    public int ArmorLevel;
    public int ArmorHP;
    public int currentArmorHP;

    public override void Interact(GameObject player) {
        Inventory playerInventory = player.GetComponent<Inventory>();
        //ºó ½½·Ô¿¡ ¸ÕÀú Âø¿ë
        switch (type) {
            case Type.helmet:
                if(playerInventory.Helmet == null) {
                    playerInventory.Helmet = this;
                    equiped = true; 
                    Pick(player.transform);
                }
                return;
            case Type.armor:
                if(playerInventory.Armor == null) {
                    playerInventory.Armor = this;
                    equiped= true;
                    Pick(player.transform);
                }
                return;
        }
        //°¡¹æ¿¡ ³Ö±â
        base.Interact(player);
    }

    public override void Thrown() {
        if (equiped) {
            if(type == Type.helmet)
                transform.parent.GetComponent<Inventory>().Helmet = null;
            if (type == Type.armor)
                transform.parent.GetComponent<Inventory>().Armor = null;
        }

        base.Thrown();
    }
}
