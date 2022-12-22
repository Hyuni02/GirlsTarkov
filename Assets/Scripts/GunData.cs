using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/GunData")]
public class GunData : FireArm
{
    public string GunName;          //이름
    public string GunDescription;   //설명
    public float Damage;            //피해량
    public AmmoType ammoType;       //사용 탄약
    public float RPM;               //발사 속도
    public float range;             //유효사거리
    public float MOA;               //탄퍼짐(견착)
    public float Recoil;            //반동(조준)
    public ActionType actionType;   //작동방식
    public ReloadType reloadType;   //장전방식
    public bool BulletInChamber;    //약실
}
