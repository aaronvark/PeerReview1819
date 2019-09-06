using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    private ObjectPoolManager objectPool;
    [SerializeField] List<Pool> spawnables;

    [Space]
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
        for (int i = 0; i < spawnables.Count; i++)
        {
            objectPool.AddPool(spawnables[i]);
        }

        spawnCollider = GetComponent<BoxCollider>();

        for (int i = 0; i < Random.Range(spawnAmountRange.x, spawnAmountRange.y); i++)
        {
            SpawnEnemy();
            //Debug.Log("Spawn Enemy");
        }
    }

    private void SpawnEnemy()
    {
        Pool _poolToSpawn = spawnables[Random.Range(0, spawnables.Count)];


        objectPool.SpawnFromPool(_poolToSpawn, randomPointInBounds(spawnCollider.bounds), _poolToSpawn.prefab.transform.rotation);
    }
}
