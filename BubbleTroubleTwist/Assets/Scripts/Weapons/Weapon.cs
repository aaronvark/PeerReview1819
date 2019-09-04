using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class that stores each weapon data
/// </summary>
[System.Serializable]
public class WeaponData
{
    public string projectileName;
    public Transform firePoint;
    public int damage;
    public float amount;
}

/// <summary>
/// Weapon class with all weapon functionality
/// </summary>
public class Weapon : MonoBehaviour
{
    public WeaponData thisWeaponData;
    public ObjectPooler objectPooler;
    private bool ready = true;
    float amount = 10;
    
    /*
    string currentProjectileName;
    Transform firePoint;
    Projectile currentProjectile;
    int damage;
    bool ready = true;

    public Weapon(string _currentProjectileName,Transform _firePoint, Projectile _projectile, int _damage)
    {
        currentProjectileName = _currentProjectileName;
        firePoint = _firePoint;
        currentProjectile = _projectile;
        damage = _damage;
    }*/
    public void FireWeapon(int _damage)
    {
        GameObject projectile = objectPooler.SpawnFromPool(thisWeaponData.projectileName, thisWeaponData.firePoint.position, Quaternion.identity);
        if(projectile != null)
        {
            projectile.GetComponent<Projectile>().damage = thisWeaponData.damage;
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
