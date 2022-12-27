using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject main_Weapon;
    //public GameObject sub_Weapon;

    public GameObject vest;
    public GameObject bag;

    private void Start() {
        main_Weapon = GetComponentInChildren<Gun>().gameObject;
        vest = GameObject.Find("Vest1");
        bag = GameObject.Find("Bag1");
    }
}
