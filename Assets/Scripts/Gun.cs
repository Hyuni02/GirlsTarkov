using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] GunData data;
    [SerializeField] Magazine mag;

    StarterAssetsInputs input;
    int magindex = 0;

    private void Start() {
        input= GetComponentInParent<StarterAssetsInputs>();
    }

    public void Shoot() {
        if(mag.count> 0) {
            mag.count--;
            Debug.Log("Fire Bullet from " + mag.name);
        }
        else {
            Debug.Log("Mag is Empty");
        }
        input.shoot = false;
    }
    public void Reload() {
        magindex++;
        if (magindex == transform.childCount) {
            magindex = 0;
        }
        mag = transform.GetChild(magindex).GetComponent<Magazine>();
        Debug.Log("Change Mag to " + mag.name);
        input.reload = false;
    }
}
