using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Player : AbstractAvatarClass 
{
    public Transform firePoint;
    public Projectile currentProjectile;
    public float cooldown;
    public float timeBetween;
    public string currentProjectileName;

    public WeaponData usingWeaponData;
    public ObjectPooler objectPooler;

    Weapon currentWeapon;

    public bool ready = true;

    void Awake()
    {
        //currentWeapon = new Weapon(currentProjectileName, firePoint, currentProjectile, damage);
        currentWeapon = new Weapon();
        currentWeapon.thisWeaponData = usingWeaponData;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentWeapon.WeaponReady()) 
            Fire();
    }

    private void Fire()
    {
        currentWeapon.objectPooler = objectPooler;
        StartCoroutine(currentWeapon.WaitForCooldown(cooldown));
    }
    
}
