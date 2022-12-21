using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] float normalSensitivity = 1f;
    [SerializeField] float aimSensitivity = 0.5f;
    
    GameObject crosshair;

    ThirdPersonController thirdPersonController;
    StarterAssetsInputs starterAssetsInputs;

    private void Awake() {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Start() {
        crosshair = GameObject.FindGameObjectWithTag("crosshair");
    }

    private void Update() {
        if (starterAssetsInputs.aim) {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            crosshair.gameObject.SetActive(true);
        }
        else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            crosshair.SetActive(false);
        }
    }
}
