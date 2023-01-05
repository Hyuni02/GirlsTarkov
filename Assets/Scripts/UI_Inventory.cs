using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour, IPointerClickHandler {

    public static UI_Inventory instance;

    [Header("Right Click Menu")]
    public GameObject RClickMenu;
    public Button button_Discard;

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
    public GameObject Group_Equip;
    public GameObject Group_PrimaryWeapon;
    public GameObject Group_Bag;
    public TMP_Text Text_LootSize;
    public Image Image_InLoot;
    public Image Image_InLoot_Helmet;
    public Image Image_InLoot_Armor;
    public Image Image_InLoot_Bag;
    public Image Image_InLoot_PrimaryWeapon;

    private void Awake() {
        instance = this;
        RClickMenu.SetActive(false);
    }

    public void UpdateAll() {
        UpdatePlayer();
        UpdateBag();
    }
    public void UpdatePlayer() {
        Inventory inven = RoomManager.instance.LocalPlayer.GetComponent<Inventory>();
        //캐릭터 아이콘
        Image_Player.sprite = inven.PlayerIcon;
        //헬멧
        if (inven.Helmet != null) {
            Image_Helmet.sprite = inven.Helmet.GetComponent<ItemInfo>().ItemIcon;
            Image_Helmet.GetComponent<Clickable>().info = inven.Helmet.GetComponent<ItemInfo>();
        }
        else
            Image_Helmet.sprite = null;
        //방어구
        if (inven.Armor != null) {
            Image_Armor.sprite = inven.Armor.GetComponent<ItemInfo>().ItemIcon;
            Image_Armor.GetComponent<Clickable>().info = inven.Armor.GetComponent<ItemInfo>();
        }
        else
            Image_Armor.sprite = null;
        //가방
        if (inven.bag != null) {
            Image_Bag.sprite = inven.bag.GetComponent<ItemInfo>().ItemIcon;
            Image_Bag.GetComponent<Clickable>().info = inven.bag.GetComponent<ItemInfo>();

        }
        else
            Image_Bag.sprite = null;
        //주무기, 부착물
        if (inven.main_Weapon != null) {
            Image_PrimaryWeapon.sprite = inven.main_Weapon.GetComponent<ItemInfo>().ItemIcon;
            Image_PrimaryWeapon.GetComponent<Clickable>().info = inven.main_Weapon.GetComponent<ItemInfo>();
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
        //보조무기
        //if(inven.sub_Weapon != null)
        //    Image_SecondaryWeapon.sprite = sub_Weapon.GetComponent<ItemInfo>().ItemIcon;
        //else
        //    Image_SecondaryWeapon.sprite = null;
    }
    public void UpdateBag() {
        Inventory inven = RoomManager.instance.LocalPlayer.GetComponent<Inventory>();
        //가방이 없으면 없다고 표시
        if (inven.bag == null) {
            Group.SetActive(false);
        }
        //가방이 있으면 가방 내 아이템 표시
        else {
            
            Text_Size.text = inven.bag.currentSize.ToString() + "/" + inven.bag.MaxSize.ToString();

            for(int i=0; i<Image_Inbag.transform.childCount; i++){
                Destroy(Image_Inbag.rectTransform.GetChild(i).gameObject);
            }

            for(int i=0; i<inven.bag.transform.childCount; i++) {
                Image item = Instantiate(Image_Item).GetComponent<Image>();
                item.sprite = inven.bag.transform.GetChild(i).GetComponent<ItemInfo>().ItemIcon;
                item.transform.SetParent(Image_Inbag.transform);
                item.GetComponent<Clickable>().info = inven.bag.transform.GetChild(i).GetComponent<ItemInfo>();
            }

            Group.SetActive(true);
        }
    }
    public void UpdateLoot(Inventory box = null) {
        if (box == null) {
            Group_Equip.SetActive(false);
            Group_Bag.SetActive(false);
            Group_PrimaryWeapon.SetActive(false);
        }
        else {
            switch (box.GetComponent<LootBox>()?.type) {
                case ItemInfo.Type.box:
                    Group_Bag.SetActive(true);
                    Debug.Log("가방 속 내용물 보여주기");
                    Text_LootSize.text = box.bag.currentSize.ToString() + "/" + box.GetComponent<Inventory>().bag.MaxSize.ToString();

                    for (int i = 0; i < Image_InLoot.transform.childCount; i++) {
                        Destroy(Image_InLoot.rectTransform.GetChild(i).gameObject);
                    }

                    for (int i = 0; i < box.bag.transform.childCount; i++) {
                        Image item = Instantiate(Image_Item).GetComponent<Image>();
                        item.sprite = box.bag.transform.GetChild(i).GetComponent<ItemInfo>().ItemIcon;
                        item.transform.SetParent(Image_InLoot.transform);
                        item.GetComponent<Clickable>().info = box.bag.transform.GetChild(i).GetComponent<ItemInfo>();
                    }
                    break;
                case ItemInfo.Type.body:
                    Group_Equip.SetActive(true);
                    Group_Bag.SetActive(true);
                    Group_PrimaryWeapon.SetActive(true);
                    Debug.Log("인벤 속 내용물 보여주기");
                    break;
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Left) {
            CloseRClickMenu();
        }
    }

    ItemInfo target;
    public void OpenRClickMenu(ItemInfo info) {
        //Debug.Log("Right Click " + info.ItemName);
        target = info;

        RClickMenu.GetComponent<RectTransform>().transform.position = Mouse.current.position.ReadValue();

        RClickMenu.SetActive(true);
    }
    public void Discard() {
        CloseRClickMenu();
        //Debug.Log("이제 여기를 구현하면 된다");
        target.Thrown();
    }
    public void CloseRClickMenu() {
        RClickMenu.SetActive(false);
    }
}
