using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    private float timestamp = 0.0f;
    [SerializeField]
    private Weapon chosenWeapon;
    [SerializeField]
    private Transform bulletSpawnPosition;

    private void Start() {
        timestamp = Time.time + 0.0f;   
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            if (Time.time > timestamp) {
                Fire();
            }
        }
    }

    // Fires weapon
    private void Fire() { 
        timestamp = Time.time + (chosenWeapon.rateOfFire / 60);
        GameObject _tempBullet = Instantiate(chosenWeapon.weaponEntity, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
    }
}
