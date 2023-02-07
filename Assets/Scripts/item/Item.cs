using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType { item, weapon, bag, loot}
    [Header("Interaction Option")]
    public bool pick;
    public bool open;

    [Header("Basic Info")]
    public ItemType itemType;
    public Sprite Item_Icon;
    public string Item_Name;
    public string Item_Description;
    public int Item_Index;
    public float Item_Size;
    public float Item_Weight;

    public void Interact(GameObject player, string option = "") {
        switch(option) {
            case "pick":
                print("Pick : " + gameObject.name);
                player.GetComponent<ThirdPersonShooterController>().PickItem(gameObject);
                break;
            case "open":
                print("Open : " + gameObject.name);
                //해당 대상으로 인벤 열기
                Debug.LogError("선택 대상으로 인벤 열기 구현 요망");
                break;
        }
    }

    public void Discard() {
        print("Discard : " + gameObject.name);
        transform.parent.GetComponent<ThirdPersonShooterController>().DiscardItem(gameObject);
    }
}
