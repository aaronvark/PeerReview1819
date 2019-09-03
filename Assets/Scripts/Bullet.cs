using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    ObjectPoolManager poolManager;

    [SerializeField] float destroyTime;

    [HideInInspector] public int bulletDamage; //Will be assigned by gun firing the bullet.
    //public float bulletForce;

    public Vector3 bulletDirection;

    Rigidbody rb;

    private void Start()
    {
        poolManager = ObjectPoolManager.Instance;
        rb = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        StartCoroutine(DisableAfterTime());
    }

    public void OnObjectDespawn()
    {
        this.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
        GetComponent<TrailRenderer>().Clear();
    }

    /*
    public void AddForce(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction * bulletForce);
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(bulletDamage);
        }
        catch { }
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);

        OnObjectDespawn();
    }

    
}
