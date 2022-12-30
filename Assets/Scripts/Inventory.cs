using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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
}
