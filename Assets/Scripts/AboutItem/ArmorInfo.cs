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
        //�� ���Կ� ���� ����
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
        //���濡 �ֱ�
        base.Interact(player);
    }
}
