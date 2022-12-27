using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthInfo : MonoBehaviour, IDamageable {
    public float maxHealth = 100;
    public float currentHealth;

    PhotonView pv;
    PlayerManager playerManager;

    private void Start() {
        pv= GetComponent<PhotonView>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage) {
        if (!pv.IsMine)
            return;

        currentHealth -= damage;

        if(currentHealth <= 0 ) {
            Die();
        }

        Debug.Log(gameObject.name + " took dmg : " + damage.ToString("N0"));
    }

    public void Die() {
        Debug.Log(gameObject.name + " Died");
        playerManager = PhotonView.Find((int)pv.InstantiationData[0]).GetComponent<PlayerManager>();

        playerManager.Die();
    }
}
