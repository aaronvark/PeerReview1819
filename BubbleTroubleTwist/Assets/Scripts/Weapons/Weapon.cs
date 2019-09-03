using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string projectileName;
    public Transform firePoint;
    public int damage;
}

public class Weapon : MonoBehaviour
{
    public WeaponData thisWeaponData;
    public ObjectPooler objectPooler;
    private string projectileName;
    private Transform firePoint;
    private int damage;
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
    private void Start()
    {


    }
    public void FireWeapon(int _damage)
    {
         objectPooler.SpawnFromPool(projectileName, firePoint.position, Quaternion.identity);            
    }

    public IEnumerator WaitForCooldown(float _time)
    {
        projectileName = thisWeaponData.projectileName;
        firePoint = thisWeaponData.firePoint;
        damage = thisWeaponData.damage;
        ready = false;
        while (amount > 1)
        {
            FireWeapon(damage);
            yield return new WaitForSeconds(0.2f);
            amount--;
        }
        yield return new WaitForSeconds(_time);
        ready = true;
        amount = 10;
    }

    public bool WeaponReady()
    {
        return ready;
    }

    /*
    public bool WeaponReady(float _cooldown)
    {
        if (ready)
            WaitForCooldown(_cooldown);

        return ready;
    }

    private IEnumerator WaitForCooldown(float _time)
    {
        while (!ready)
        {
            _time -= Time.time;
            if (_time < 0.1f) { ready = true; yield return new WaitForSeconds(0.1f); }
        }
    }*/
}
