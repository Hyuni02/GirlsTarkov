using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum WeaponType { Primary, Secondary }

    [Header("Weapon Info")]
    public WeaponType weaponType;
    public Sprite equipedIcon;

    public void Fire() {

    }
    public void Aim() {

    }
    public void Eject() {

    }
    public void Reload() {

    }
}
