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
public class Weapon : MonoBehaviour
{
    public WeaponData thisWeaponData;

    private bool ready = true;
    private float amount = 10;

    //GenericObjectPooler<Projectile> projectilePool;
    
    private void Start()
    {
        //if(projectilePool == null)
        //projectilePool = new GenericObjectPooler<Projectile>(thisWeaponData.projectileGameObject, 30);
        EventManager.AddHandler(EVENT.MyEvent2, StopOnHit);
    }

    public void FireWeapon(int _damage)
    {
        //GameObject projectile = ObjectPooler.Instance.SpawnFromPool(thisWeaponData.projectileName, thisWeaponData.firePoint.position, Quaternion.identity);
        //GameObject projectile = projectilePool.GetNext().gameObject;
        //var bulletPool = FindObjectOfType<BulletPool>();
        //GameObject prjctile = bulletPool.Get().gameObject;
        //projectile.transform.position = thisWeaponData.firePoint.position;
        GameObject projectile = ProjectilePool.Instance.SpawnFromPool(thisWeaponData.firePoint.position, Quaternion.identity);
        /*if (projectile != null)
        {
            projectile.gameObject.GetComponent<Projectile>().damage = thisWeaponData.damage;
        }*/
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
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        EventManager.OnEnemyHitHandler -= StopOnHit;
    }
}
