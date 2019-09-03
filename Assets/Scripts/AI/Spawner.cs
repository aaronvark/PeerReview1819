using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    ObjectPoolManager objectPool;

    [Tooltip("Enemy prefabs that can be spawned.")]
    [SerializeField] GameObject[] spawnables;

    BoxCollider spawnCollider;
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
    }

    void SpawnEnemy()
    {
        
    }
}
