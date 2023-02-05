using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
//using UnityEditor.Rendering;

public class ThirdPersonShooterController : MonoBehaviour {
    CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity = 1f;
    [SerializeField] float aimSensitivity = 0.5f;
    public LayerMask aimColliderMask;
    public LayerMask interactColliderMask;

    [HideInInspector]
    public Transform debugTransform;

    GameObject crosshair;

    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    Inventory inventory;

    private void Awake() {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        inventory= GetComponent<Inventory>();
    }

    private void Start() {
        GameObject.FindGameObjectWithTag("NormalCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0);
        aimVirtualCamera = GameObject.FindGameObjectWithTag("AimCamera").GetComponent<CinemachineVirtualCamera>();
        aimVirtualCamera.Follow = transform.GetChild(0);
        crosshair = GameObject.FindGameObjectWithTag("crosshair");
        debugTransform = GameObject.Find("Sphere").transform;
    }

    private void Update() {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderMask)) {
            mouseWorldPosition = hit.point;
        }

        if (starterAssetsInputs.aim) {
            if (inventory.main_Weapon != null) {
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.SetSensitivity(aimSensitivity);
                thirdPersonController.SetRotateOnMove(false);
                crosshair.gameObject.SetActive(true);
                animator.SetBool("Aim", true);
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 20f));

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

                if (starterAssetsInputs.shoot) {
                    if (inventory.main_Weapon != null) {
                        inventory.main_Weapon.GetComponent<Gun>().Shoot();
                    }
                    starterAssetsInputs.shoot = false;
                }
            }
        }
        else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            crosshair.SetActive(false);
            animator.SetBool("Aim", true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 20f));
        }



        if (starterAssetsInputs.reload) {
            if(inventory.main_Weapon != null) {
                inventory.main_Weapon.GetComponent<Gun>().Reload();
            }
            starterAssetsInputs.reload= false;
        }

        if (starterAssetsInputs.interact) {
            if (Physics.Raycast(ray, out RaycastHit _hit, 15f, interactColliderMask)) {
                _hit.collider.GetComponent<ItemInfo>()?.Interact(gameObject);
                RoomManager.instance.LocalPlayer.GetComponent<Inventory>().lastInteract = _hit.transform;
            }
            starterAssetsInputs.interact = false;
        }

        if (starterAssetsInputs.inventory) {
            InventoryViewer invenViewer = GetComponent<InventoryViewer>();
            if (invenViewer.open) {
                invenViewer.CloseInventory();
            }
            else {
                invenViewer.OpenInventory();
            }
            starterAssetsInputs.inventory = false;
        }
    }

    public void Escape() {

    }


}
