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

    private void Awake() {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
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
            debugTransform.position = hit.point;
            mouseWorldPosition = hit.point;
        }

        if (starterAssetsInputs.aim) {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            crosshair.gameObject.SetActive(true);

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
        }

    }
}
