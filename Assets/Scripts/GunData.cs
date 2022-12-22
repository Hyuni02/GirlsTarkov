using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/GunData")]
public class GunData : FireArm
{
    public string GunName;          //�̸�
    public string GunDescription;   //����
    public float Damage;            //���ط�
    public AmmoType ammoType;       //��� ź��
    public float RPM;               //�߻� �ӵ�
    public float range;             //��ȿ��Ÿ�
    public float MOA;               //ź����(����)
    public float Recoil;            //�ݵ�(����)
    public ActionType actionType;   //�۵����
    public ReloadType reloadType;   //�������
    public bool BulletInChamber;    //���
}
