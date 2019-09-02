using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Player : AbstractAvatarClass 
{
    public Transform firePoint;
    public Projectile currentProjectile;
    public float cooldown;

    Weapon currentWeapon;

    public Player()
    {
        currentWeapon = new Weapon(firePoint, currentProjectile, damage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentWeapon.WeaponReady(cooldown)) 
            Fire();
    }

    

    private void Fire()
    {
        currentWeapon.FireWeapon(damage);
    }
}
