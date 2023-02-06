using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum WeaponType { Primary, Secondary }

    [Header("Weapon Type")]
    public WeaponType weaponType;
}
