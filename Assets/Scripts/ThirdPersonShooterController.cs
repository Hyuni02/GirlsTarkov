using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour {
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity = 1f;
    [SerializeField] float aimSensitivity = 0.5f;
    [SerializeField] LayerMask aimColliderMask;
    [SerializeField] Transform debugTransform;

    GameObject crosshair;

    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;

    private void Awake() {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
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
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            crosshair.gameObject.SetActive(true);
            animator.SetBool("Aim", true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1),1f,Time.deltaTime * 20f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            crosshair.SetActive(false);
            animator.SetBool("Aim", true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 20f));
        }

        if (starterAssetsInputs.shoot) {
            if (Physics.Raycast(ray, out RaycastHit _hit, 999f, aimColliderMask)) {
                GetComponentInChildren<Gun>().Shoot();
                debugTransform.position = _hit.point;
            }
            starterAssetsInputs.shoot = false;
        }

        if (starterAssetsInputs.reload) {
            GetComponentInChildren<Gun>().Reload();
            starterAssetsInputs.reload= false;
        }

    }
}
