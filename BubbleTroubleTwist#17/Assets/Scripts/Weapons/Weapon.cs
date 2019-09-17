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

    private bool ready = true;
    private float amount = 10;
    private float speed = 5f;
    public Weapon(WeaponData _thisWeaponData)
    {
        thisWeaponData = _thisWeaponData;
    }
    
    private void Start()
    {
        //if(projectilePool == null)
        //projectilePool = new GenericObjectPooler<Projectile>(thisWeaponData.projectileGameObject, 30);
        EventManager.OnLevelUpdateHandler += StopOnHit;
    }

    public void FireWeapon(int _damage)
    {

        //GameObject projectile = ObjectPoolerLearning.Instance.SpawnFromPool<Projectile>(thisWeaponData.firePoint.position, Quaternion.identity);
        GameObject projectile = ObjectPoolerTypes.Instance.SpawnFromPool(thisWeaponData.projectileGameObject, thisWeaponData.firePoint.position, Quaternion.identity);
        if (projectile != null)
        {
            projectile.gameObject.GetComponent<Projectile>().damage = thisWeaponData.damage;
            projectile.transform.LerpTransform(new Vector3(projectile.transform.position.x, projectile.transform.position.y + 10f, projectile.transform.position.z), speed);
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

    private void StopOnHit()
    {
        //StopCoroutine(WaitForCooldown());
        //StopAllCoroutines();
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyHitHandler -= StopOnHit;
    }
}
