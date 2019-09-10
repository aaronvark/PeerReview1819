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
    private float spawnHeight;
    private int randomSpawnNumber;

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

        spawnHeight = spawnCollider.bounds.center.y;

        randomSpawnNumber = (int)Random.Range(spawnAmountRange.x, spawnAmountRange.y);

        for (int j = 0; j < spawnables.Count; j++)
        {
            objectPool.AddPool(spawnables[j]);
        }

        for (int j = 0; j < 3; j++)
        {
            SpawnRow();
        }   
    }

    void SpawnRow()
    {
        float _difference = spawnCollider.bounds.size.x / randomSpawnNumber;

        for (int i = 0; i < randomSpawnNumber; i++)
        {
            Vector3 _spawnPos = new Vector3(spawnCollider.bounds.min.x + _difference * i, spawnHeight, spawnCollider.bounds.center.z);

            SpawnEnemy(_spawnPos);
        }

        spawnHeight += 100f;

    }

    private void SpawnEnemy(Vector3 _spawnPosition)
    {
        Pool _poolToSpawn = spawnables[Random.Range(0, spawnables.Count)];

        objectPool.SpawnFromPool(_poolToSpawn, _spawnPosition, _poolToSpawn.prefab.transform.rotation);
    }
}
