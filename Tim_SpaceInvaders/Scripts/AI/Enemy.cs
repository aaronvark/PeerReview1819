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

    [SerializeField]
    private float timer = 2;

    private void Start()
    {   
        objectPool = ObjectPoolManager.Instance;

        if (!objectPool.pools.Contains(bulletPool) && !objectPool.pools.Contains(particlePool))
        {
            objectPool.AddPool(bulletPool);
            objectPool.AddPool(particlePool);
        }
        

        target = Player.Instance.gameObject;

        moveSpeed = moveSpeed * Random.Range(1f, 1.2f);
    }

    public void OnObjectSpawn()
    {
        timer = Random.Range(maxWaitTimeForShot/4, maxWaitTimeForShot);
    }

    public void OnObjectDespawn()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        MoveToTarget();
        if (Timer() < 0)
        {
            //Attack();
        }
    }
        
    private void MoveToTarget()
    {
        float _prefferedHeight = 100f;
        float _targetDistance = Vector3.Distance(transform.position, target.transform.position);
        float _height = transform.position.y;

        Vector3 _forwardPostion;

        if (_targetDistance >= stopDistance){
            // Debug.DrawRay(transform.position, -transform.forward * 10, Color.red);

            if(_height > _prefferedHeight)
            {
                _forwardPostion = transform.position + new Vector3(0, -3, -2);
            }
            else
            {
                _forwardPostion = transform.position + -transform.forward;
            }


            //TODO meer verspreiding van enemies over eindlocatie.
            transform.position = Vector3.Lerp(transform.position, _forwardPostion, Time.deltaTime * moveSpeed);
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

    private void Attack()
    {
        //yield return new WaitForSeconds(Random.Range(maxWaitTimeForShot/2, maxWaitTimeForShot));

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Vector3 _targetDirection = target.transform.position - spawnPositions[i].transform.position;

            GameObject _bulletClone = objectPool.SpawnFromPool(bulletPool, spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            _bulletClone.GetComponent<Bullet>().bulletDamage = bulletDamage;

            _bulletClone.GetComponent<Rigidbody>().AddForce(_targetDirection * bulletForce);

        }

       timer = Random.Range(2, maxWaitTimeForShot);

    }

    private float Timer()
    {
        return timer -= Time.deltaTime;
    }


}
