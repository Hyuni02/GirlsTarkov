using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using static UnityEditor.Progress;
using System.Reflection;

public class ThirdPersonShooterController : MonoBehaviour {
    CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity = 1f;
    [SerializeField] float aimSensitivity = 0.5f;
    public LayerMask aimColliderMask;
    public LayerMask InteractColliderMask;

    [HideInInspector]
    public Transform debugTransform;

    GameObject crosshair;

    GameObject inventory;

    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    Transform beforeitem;
    float mouseScrollY;

    private void Awake() {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        inventory = GameObject.Find("Canvas_Inventory");
        inventory.SetActive(false);
    }

    private void Start() {
        GameObject.FindGameObjectWithTag("NormalCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0);
        //aimVirtualCamera = GameObject.FindGameObjectWithTag("AimCamera").GetComponent<CinemachineVirtualCamera>();
        //aimVirtualCamera.Follow = transform.GetChild(0);
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

        //조준, 발사, 장전
        {

            //if (starterAssetsInputs.aim) {
            //    aimVirtualCamera.gameObject.SetActive(true);
            //    thirdPersonController.SetSensitivity(aimSensitivity);
            //    thirdPersonController.SetRotateOnMove(false);
            //    crosshair.gameObject.SetActive(true);
            //    animator.SetBool("Aim", true);
            //    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1),1f,Time.deltaTime * 20f));

            //Vector3 worldAimTarget = mouseWorldPosition;
            //worldAimTarget.y = transform.position.y;
            //Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            //transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            //}
            //else {
            //    aimVirtualCamera.gameObject.SetActive(false);
            //    thirdPersonController.SetSensitivity(normalSensitivity);
            //    thirdPersonController.SetRotateOnMove(true);
            //    crosshair.SetActive(false);
            //    animator.SetBool("Aim", true);
            //    animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 20f));
            //}

            //if (starterAssetsInputs.shoot) {
            //    inventory.main_Weapon.GetComponent<Gun>().Shoot();
            //    starterAssetsInputs.shoot = false;
            //}

            //if (starterAssetsInputs.reload) {
            //    inventory.main_Weapon.GetComponent<Gun>().Reload();
            //    starterAssetsInputs.reload= false;
            //}
        }

        //인벤 여닫기
        if (starterAssetsInputs.inventory) {
            OpenCloseInventory();
            starterAssetsInputs.inventory = false;
        }

        //아이템 상호작용
        RaycastHit hit_interact;
        if(Physics.Raycast(ray, out hit_interact, 4f, InteractColliderMask)) {
            InteractionOptionViewer ioviewer = Canvas_Inventory.Instance.IOViewer.GetComponent<InteractionOptionViewer>();
            //상호작용 옵션 표시
            Canvas_Inventory.Instance.IOViewer.SetActive(true);

            if (hit_interact.transform != beforeitem) {
                //대상 변경 시 옵션 변경
                ioviewer.UpdateInteractionOption(
                    hit_interact.transform.GetComponent<Item>().pick,
                    hit_interact.transform.GetComponent<Item>().open);
                beforeitem = hit_interact.transform;

                ioviewer.index = 0;
                ioviewer.HighlightIndex();
            }

            //상호작용 옵션 선택
            if (starterAssetsInputs.scroll > 0) {
                ioviewer.index++;
            }
            if(starterAssetsInputs.scroll< 0) {
                ioviewer.index--;
            }
            if (ioviewer.beforeindex != ioviewer.index) {
                ioviewer.HighlightIndex();
                ioviewer.beforeindex = ioviewer.index;
            }

            //상호작용 확정
            if (starterAssetsInputs.interact) {
                hit_interact.transform.GetComponent<Item>()?.Interact(gameObject,ioviewer.option);
            }
        }
        else {
            Canvas_Inventory.Instance.IOViewer.SetActive(false);
        }
        starterAssetsInputs.interact = false;

    }

    public void Escape() {

    }

    public void OpenCloseInventory(GameObject target = null) {
        if (inventory.activeSelf == true) {
            inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else {
            inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void PickItem(GameObject item) {
        item.transform.SetParent(transform);
        Image i = Instantiate(Canvas_Inventory.Instance.Image_Item);

        switch (item.GetComponent<Item>().itemType) {
            case Item.ItemType.weapon:
                switch (item.GetComponent<Weapon>().weaponType) {
                    case Weapon.WeaponType.Primary:
                        if (Canvas_Inventory.Instance.pri_weapon.childCount == 0) {
                            i.transform.SetParent(Canvas_Inventory.Instance.pri_weapon);
                        }
                        else
                            i.transform.SetParent(Canvas_Inventory.Instance.bag);
                        break;
                    case Weapon.WeaponType.Secondary:
                        if (Canvas_Inventory.Instance.sec_weapon.childCount == 0) {
                            i.transform.SetParent(Canvas_Inventory.Instance.sec_weapon);
                        }
                        else
                            i.transform.SetParent(Canvas_Inventory.Instance.bag);
                        break;
                }
                break;
            //case Item.ItemType.bag:
            //    if (Canvas_Inventory.Instance.bag.childCount == 0) {
            //        i.transform.SetParent(Canvas_Inventory.Instance.bag);
            //    }
            //    else
            //        i.transform.SetParent(Canvas_Inventory.Instance.bag);
            //    break;
            default:
                i.transform.SetParent(Canvas_Inventory.Instance.bag);
                break;
        }

        i.sprite = item.GetComponent<Item>().Item_Icon;
        i.GetComponent<Clickable>().real = item;

        item.GetComponent<MeshRenderer>().enabled = false;
        item.GetComponent<Collider>().enabled = false;
        Destroy(item.GetComponent<Rigidbody>());
    }

    public void DiscardItem(GameObject item) {
        item.transform.SetParent(null);

        item.GetComponent<MeshRenderer>().enabled = true;
        item.GetComponent<Collider>().enabled = true;
        item.AddComponent<Rigidbody>();

        item.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
    }
}
