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
    
    private void Awake()
    {
        //Create a weapon and give its weapon data
        currentWeapon = new Weapon(usingWeaponData);
    }

    private void OnEnable()
    {
        //if(projectilePool == null)
        //projectilePool = new GenericObjectPooler<Projectile>(thisWeaponData.projectileGameObject, 30);
        //EventManager.OnLevelUpdateHandler += StopOnHit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(PlayerInput.fireKey) && currentWeapon.WeaponReady())
            Fire();
    }

    private void Fire()
    {
        Coroutine cooldownRoutine = StartCoroutine(currentWeapon.WaitForCooldown(cooldown, timeBetween));
    }

    private void StopOnHit()
    {
        StopAllCoroutines();
        currentWeapon.ready = true;
    }

    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
        EventManager.OnLevelUpdateHandler -= StopOnHit;
    }
}
