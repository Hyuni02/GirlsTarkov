using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : ItemInfo
{
    [SerializeField] MagazineData data;
    public int count;
    public float magWeight;

    private void Start() {
        ItemName= data.name;
    }

    //탄약 삽입 후 무게 조절
}
