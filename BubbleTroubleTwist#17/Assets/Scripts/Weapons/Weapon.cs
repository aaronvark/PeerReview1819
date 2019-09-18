using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class that stores each weapon data
/// </summary>
[System.Serializable]
public class WeaponData
{
    public GameObject projectileGameObject;
    public string projectileName;
    public Transform firePoint;
    public int damage;
    public float amount;
}

/// <summary>
/// Weapon class with all weapon functionality
/// </summary>
public class Weapon 
{
    public WeaponData thisWeaponData { get; set; }

    public bool ready = true;

    private float amount = 10;
    public Weapon(WeaponData _thisWeaponData)
    {
        thisWeaponData = _thisWeaponData;
    }
   

    public void FireWeapon(int _damage)
    {

        GameObject projectile = ObjectPoolerLearning.Instance.SpawnFromPool<Projectile>(thisWeaponData.firePoint.position, Quaternion.identity).gameObject;
        //GameObject projectile = ObjectPoolerTypes.Instance.SpawnFromPool(thisWeaponData.projectileGameObject, thisWeaponData.firePoint.position, Quaternion.identity);
        if (projectile != null)
        {
            projectile.gameObject.GetComponent<Projectile>().damage = thisWeaponData.damage;
        }
    }

    public IEnumerator WaitForCooldown(float _time, float _timeBetween)
    {
        amount = thisWeaponData.amount;
        ready = false;
        while (amount > 1)
        {
            FireWeapon(thisWeaponData.damage);
            yield return new WaitForSeconds(_timeBetween);
            amount--;
        }
        yield return new WaitForSeconds(_time);
        ready = true;
        amount = thisWeaponData.amount;
    }

    public bool WeaponReady()
    {
        return ready;
    }
}
