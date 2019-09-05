using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player class with all player related functionality
/// </summary>
public class Player : AbstractAvatarClass
{
    public Transform firePoint;
    public float cooldown;
    public float timeBetween;
    public string currentProjectileName;

    public WeaponData usingWeaponData;

    Weapon currentWeapon;

    void Awake()
    {
        //Create a weapon and give its weapon data
        currentWeapon = new Weapon();
        currentWeapon.thisWeaponData = usingWeaponData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentWeapon.WeaponReady())
            Fire();
    }

    private void Fire()
    {
        StartCoroutine(currentWeapon.WaitForCooldown(cooldown, timeBetween));
    }

    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }
}
