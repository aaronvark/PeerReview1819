using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    private ObjectPoolManager objectPool;

    [SerializeField] private Vector2 spawnAmountRange;

    private BoxCollider spawnCollider;

    public static Vector3 randomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void Start()
    {
        objectPool = ObjectPoolManager.Instance;

        spawnCollider = GetComponent<BoxCollider>();

        for (int i = 0; i < Random.Range(spawnAmountRange.x, spawnAmountRange.y); i++)
        {
            SpawnEnemy();
            Debug.Log("Spawn Enemy");
        }
    }

    private void SpawnEnemy()
    {
        //TODO make only enemy types spawn from the pool. 
        //ObjectPoolManager.Pool _randomSpawnable = objectPool.pools[Random.Range(0, objectPool.pools.Count)];
        ObjectPoolManager.Pool _randomSpawnable = objectPool.pools[1];

        objectPool.SpawnFromPool(_randomSpawnable.tag, randomPointInBounds(spawnCollider.bounds), _randomSpawnable.prefab.transform.rotation);
    }
}
