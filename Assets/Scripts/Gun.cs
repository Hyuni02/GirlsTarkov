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
    Inventory inventory;
    int magindex = 0;

    private void Start() {
        input= GetComponentInParent<StarterAssetsInputs>();
        inventory = GetComponentInParent<Inventory>();
    }

    public void Shoot() {
        if(mag == null) {
            Debug.Log("Mag is Null");
            return;
        }

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
        //±âÁ¸ÀÇ ÅºÃ¢ Á¦°Å(¾øÀ¸¸ç ³Ñ±è)
        if(mag != null) {
            mag.transform.SetParent(inventory.vset.transform);
        }

        //»õ·Î¿î ÅºÃ¢ ÀåÂø
        Transform newMag = GetComponentInParent<Inventory>().vset.transform.GetChild(0);
        mag =  newMag.GetComponent<Magazine>();
        newMag.SetParent(transform);

        Debug.Log("Change Mag to " + mag.name);
        input.reload = false;
    }
}
