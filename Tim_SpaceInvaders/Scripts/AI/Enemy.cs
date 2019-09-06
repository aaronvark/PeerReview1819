using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IPoolObject
{

    [Header("Move Settings")]
    [Tooltip("The distance from the player for which it stops.")]
    [SerializeField] float stopDistance = 200f;
    [SerializeField] float moveSpeed = 10f;

    [Header("Health Settings")]
    [SerializeField] int health = 40;

    [Header("Gun Settings")]
    [SerializeField] float maxWaitTimeForShot;

    [SerializeField] Pool bulletPool;
    [SerializeField] float bulletForce;
    [SerializeField] int bulletDamage;

    [Space]
    [Tooltip("Spawn positions of bullets (e.g. the position where the bullets come out of the rifle.")]
    [SerializeField] private GameObject[] spawnPositions;

    private GameObject target;
    private ObjectPoolManager objectPool;

    [Space]
    [Header("Death Settings")]

    //TODO pool death particles?
    [SerializeField] Pool particlePool;

    private void Start()
    {
        objectPool = ObjectPoolManager.Instance;

        if (!objectPool.pools.Contains(bulletPool) && !objectPool.pools.Contains(particlePool))
        {
            objectPool.AddPool(bulletPool);
            objectPool.AddPool(particlePool);
        }
        

        target = Player.Instance.gameObject;

        moveSpeed = moveSpeed * Random.Range(0.2f, 1.2f);
    }

    public void OnObjectSpawn()
    {
        StartCoroutine(DoShot());
    }

    public void OnObjectDespawn()
    {
        this.gameObject.SetActive(false);

        StopCoroutine(DoShot());
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        float _targetDistance = Vector3.Distance(transform.position, target.transform.position);

        if(_targetDistance >= stopDistance){

            //TODO meer verspreiding van enemies over eindlocatie.
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * moveSpeed * 0.01f);
        }
    }

    public void Damage(int damage)
    {
        if (health <= 0) {
            Die();
        }
        else{
            health -= damage;
        }
    }

    public void Die()
    {
        objectPool.SpawnFromPool(particlePool, transform.position, particlePool.prefab.transform.rotation);

        OnObjectDespawn();
    }

    private IEnumerator DoShot()
    {
       
        yield return new WaitForSeconds(Random.Range(maxWaitTimeForShot/2, maxWaitTimeForShot));

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Vector3 _targetDirection = target.transform.position - spawnPositions[i].transform.position;
            //Debug.Log(_targetDirection);

            GameObject _bulletClone = objectPool.SpawnFromPool(bulletPool, spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            _bulletClone.GetComponent<Bullet>().bulletDamage = bulletDamage;

            //_bulletClone.GetComponent<Bullet>().bulletForce = bulletForce;
            _bulletClone.GetComponent<Rigidbody>().AddForce(_targetDirection * bulletForce);

        }

        StartCoroutine(DoShot());
    }

    
}
