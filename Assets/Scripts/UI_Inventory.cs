using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [Header("Panel_Player")]
    public GameObject Panel_Player;
    public Image Image_Player;
    public Image Image_Helmet;
    public Image Image_Armor;
    public Image Image_Bag;
    public Image Image_PrimaryWeapon;
    public Image Image_Muzzle;
    public Image Image_Grip;
    public Image Image_Sight;
    public Image Image_SecondaryWeapon;

    [Header("Panel_Bag")]
    public GameObject Panel_Bag;
    public GameObject Group;
    public TMP_Text Text_Size;
    public Image Image_Inbag;
    public Image Image_Item;

    [Header("Panel_Loot")]
    public GameObject Panel_Loot;


    public void UpdateAll(Inventory inven) {
        UpdatePlayer(inven);
        UpdateBag(inven);
        UpdateLoot();
    }
    public void UpdatePlayer(Inventory inven) {
        //ĳ���� ������
        Image_Player.sprite = inven.PlayerIcon;
        //���
        if (inven.Helmet != null)
            Image_Helmet.sprite = inven.Helmet.GetComponent<ItemInfo>().ItemIcon;
        else
            Image_Helmet.sprite = null;
        //��
        if(inven.Armor != null)
            Image_Armor.sprite = inven.Armor.GetComponent<ItemInfo>().ItemIcon;
        else
            Image_Armor.sprite = null;
        //����
        if (inven.bag != null) {
            Image_Bag.sprite = inven.bag.GetComponent<ItemInfo>().ItemIcon;
        }
        else
            Image_Bag.sprite = null;
        //�ֹ���, ������
        if (inven.main_Weapon != null) {
            Image_PrimaryWeapon.sprite = inven.main_Weapon.GetComponent<ItemInfo>().ItemIcon;
            //Image_Muzzle.sprite = inven.main_Weapon.muzzle.GetComponent<ItemInfo>().ItemIcon;
            //Image_Grip.sprite = inven.main_Weapon.grip.GetComponent<ItemInfo>().ItemIcon;
            //Image_Sight.sprite = inven.main_Weapon.sight.GetComponent<ItemInfo>().ItemIcon;
        }
        else {
            Image_PrimaryWeapon.sprite = null;
            Image_Muzzle.sprite = null;
            Image_Grip.sprite = null;
            Image_Sight.sprite = null;
        }
        //��������
        //if(inven.sub_Weapon != null)
        //    Image_SecondaryWeapon.sprite = sub_Weapon.GetComponent<ItemInfo>().ItemIcon;
        //else
        //    Image_SecondaryWeapon.sprite = null;
    }
    public void UpdateBag(Inventory inven) {
        //������ ������ ���ٰ� ǥ��
        if(inven.bag == null) {
            Group.SetActive(false);
        }
        //������ ������ ���� �� ������ ǥ��
        else {
            
            Text_Size.text = inven.bag.currentSize.ToString() + "/" + inven.bag.MaxSize.ToString();

            for(int i=0; i<Image_Inbag.transform.childCount; i++){
                Destroy(Image_Inbag.rectTransform.GetChild(i).gameObject);
            }

            for(int i=0; i<inven.bag.transform.childCount; i++) {
                Image item = Instantiate(Image_Item).GetComponent<Image>();
                item.sprite = inven.bag.transform.GetChild(i).GetComponent<ItemInfo>().ItemIcon;
                item.transform.SetParent(Image_Inbag.transform);
            }

            Group.SetActive(true);
        }



    }
    public void UpdateLoot(Inventory inven = null) {

    }
}
