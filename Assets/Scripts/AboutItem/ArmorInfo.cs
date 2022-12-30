using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sprites;
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
                    Pick(player.transform);
                }
                return;
            case Type.armor:
                if(playerInventory.Armor == null) {
                    playerInventory.Armor = this;
                    Pick(player.transform);
                }
                return;
        }
        //°¡¹æ¿¡ ³Ö±â
        base.Interact(player);
    }
}
