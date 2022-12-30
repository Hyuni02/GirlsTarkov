using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : ItemInfo
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

        ItemName = data.GunName;
        ItemDescription = data.GunDescription;

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
        Magazine newMag = SearchMag();
        RemoveMag();
        InsertMag(newMag);
    }
    void RemoveMag() {
        if(mag == null) {
            Debug.Log("Gun has no Mag");
        }
        else {
            if(inventory.bag == null) {
                mag.Thrown();
            }
            else {
                if(inventory.bag.currentSize + mag.size <= inventory.bag.MaxSize) {
                    mag.MoveItem(transform, inventory.bag.transform);
                }
                else {
                    mag.Thrown();
                }
            }
        }

    }
    Magazine SearchMag() {
        if(inventory.bag != null) {
            Magazine newMag = inventory.bag.transform.GetComponentInChildren<Magazine>();
            return newMag;
        }
        else {
            Debug.Log("No Bag");
            return null;
        }
    }
    void InsertMag(Magazine newMag) {
        if (newMag != null) {
            newMag.MoveItem(inventory.bag.transform, gameObject.transform);
            mag = newMag;

            Debug.Log("Change Mag to " + mag.name);
        }
        else {
            mag = null;
            Debug.Log("No Mag In Bag");
        }
        input.reload = false;
    }

    public override void Interact(GameObject player) {
        Inventory playerInventory = player.GetComponent<Inventory>();

        if (playerInventory.main_Weapon == null) {
            playerInventory.main_Weapon = this;
            Pick(player.transform);

            inventory = playerInventory;
            input = player.GetComponent<StarterAssetsInputs>();
            thirdPersonShooterController = player.GetComponent<ThirdPersonShooterController>();

            return;
        }
        base.Interact(player);
    }

}
