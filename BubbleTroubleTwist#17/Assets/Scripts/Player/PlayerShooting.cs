using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/// <summary>
/// Player behaviour with all player shooting related functionality
/// </summary>
public class PlayerShooting : AbstractAvatarClass
{
    public Transform firePoint;
    public float cooldown;
    public float timeBetween;
    public string currentProjectileName;

    public WeaponData usingWeaponData;

    private Weapon currentWeapon;

    private void OnEnable()
    {
    }

    void Awake()
    {
        //Create a weapon and give its weapon data
        currentWeapon = new Weapon();
        currentWeapon.thisWeaponData = usingWeaponData;
    }

    private void Update()
    {
        if (Input.GetKeyDown(playerInput.fireKey) && currentWeapon.WeaponReady())
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
