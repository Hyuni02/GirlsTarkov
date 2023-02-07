using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Inventory : MonoBehaviour
{
    public static Canvas_Inventory Instance;

    [HideInInspector]
    public Canvas canvas;

    public Image Image_Item;
    public Transform bag;

    [Header("Interaction Option Viewer")]
    public GameObject IOViewer;

    [Header("Equiped")]
    public Transform helmet;
    public Transform bodyarmor;
    public Transform pri_weapon;
    public Transform sec_weapon;

    private void Awake() {
        Instance= this;
        canvas = GetComponent<Canvas>();
    }
}
