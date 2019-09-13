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

    private int rowNumber;

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

        for (int i = 0; i < 3; i++)
        {
            rowNumber = i;
            rowNumber++;

            SpawnRow();
        }   
    }

    void SpawnRow()
    {
        float _difference = spawnCollider.bounds.size.x / randomSpawnNumber;

        for (int i = 0; i < randomSpawnNumber; i++)
        {
            Vector3 _spawnPos = new Vector3(spawnCollider.bounds.min.x + _difference * i, spawnHeight, spawnCollider.bounds.center.z);

            Pool _poolToSpawn = spawnables[Random.Range(0, spawnables.Count)];

            objectPool.SpawnFromPool(_poolToSpawn, _spawnPos, _poolToSpawn.prefab.transform.rotation);

            objectPool.objectToSpawn.GetComponent<Enemy>().prefferedHeight = 100f * rowNumber;
             
        }

        spawnHeight += 100f;
    }

}
