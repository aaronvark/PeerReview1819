using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Transform firePoint;
    Projectile currentProjectile;
    int damage;
    bool ready = false;

    public Weapon(Transform _firePoint, Projectile _projectile, int _damage)
    {
        firePoint = _firePoint;
        currentProjectile = _projectile;
        damage = _damage;
    }

    public void FireWeapon(int _damage)
    {

    }
    
    public bool WeaponReady(float _cooldown)
    {
        if (ready)
            StartCoroutine(WaitForCooldown(_cooldown));

        return ready;
    }

    private IEnumerator WaitForCooldown(float _time)
    {
        while (!ready)
        {
            _time -= Time.time;
            if (_time < 0.1f) { ready = false; yield return new WaitForSeconds(0.1f); }
        }
    }
}
