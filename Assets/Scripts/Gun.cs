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
    ThirdPersonShooterController thirdPersonShooterController;

    private void Start() {
        input= GetComponentInParent<StarterAssetsInputs>();
        inventory = GetComponentInParent<Inventory>();
        thirdPersonShooterController = GetComponentInParent<ThirdPersonShooterController>();
    }

    public void Shoot() {
        if (mag == null) {
            Debug.Log("Mag is Null");
            return;
        }
        if (mag.count <= 0) {
            Debug.Log("Mag is Empty");
            return;
        }

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit _hit, 999f, thirdPersonShooterController.aimColliderMask)) {
            thirdPersonShooterController.debugTransform.position = _hit.point;
            _hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(data.Damage);
        }
        mag.count--;
        Debug.Log("Fire Bullet from " + mag.name);

        input.shoot = false;
    }


    public void Reload() {
        //±âÁ¸ÀÇ ÅºÃ¢ Á¦°Å(¾øÀ¸¸ç ³Ñ±è)
        //if(mag != null) {
        //    mag.transform.SetParent(inventory.vest.transform);
        //}

        ////»õ·Î¿î ÅºÃ¢ ÀåÂø
        //Transform newMag = GetComponentInParent<Inventory>().vest.transform.GetChild(0);
        //mag =  newMag.GetComponent<Magazine>();
        //newMag.SetParent(transform);

        //Debug.Log("Change Mag to " + mag.name);
        //input.reload = false;
    }
}
