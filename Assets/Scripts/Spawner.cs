using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private List<GameObject> list = new List<GameObject>();

    private Camera mainCamera;

    //Sets the _manCamera as the Main Camera in the scene.
    //Calls SpawnObject five times to start off.
    private void Awake()
    {
        mainCamera = Camera.main;

        for (int _aantalSpawned = 0; _aantalSpawned < 5; _aantalSpawned++)
        {
            SpawnObject();
        }
    }

    //Checks if all the enemys in the list are not null
    //If an enemy is null then remove it and spawn a new enemy in its place.
    private void Update()
    {
        foreach (GameObject _enemy in list.ToArray())
        {
            if (_enemy == null)
            {
                list.Remove(_enemy);
                SpawnObject();
            }
        }
    }
    
    //Sets the spawn location and direction of the object to be spawned.
    //Instantiates the object to be spawned and adds it to the list list.
    private void SpawnObject()
    {
        GameObject _newlySpawnedEnemy;

        Vector3 _setVector;

        int _horizontalSpawn = Random.Range(0, 2);

        int _spawnSide = Random.Range(0, 2);

        if (_horizontalSpawn == 1)
        {
            _setVector = new Vector3(_spawnSide == 0 ? 0 : 1, Random.Range(-0.3f, 1.3f), 0);
        }
        else
        {
            _setVector = new Vector3(Random.Range(-0.3f, 1.3f), _spawnSide == 0 ? 0 : 1, 0);
        }

        Vector3 _spawnPos = mainCamera.ViewportToWorldPoint(_setVector);
        _spawnPos.z = 0;

        _newlySpawnedEnemy = Instantiate(objectToSpawn, _spawnPos, Quaternion.identity);
        list.Add(_newlySpawnedEnemy);
    }
}
