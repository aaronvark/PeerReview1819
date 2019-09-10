using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidObject;
    private static AsteroidSpawner instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        PoolManager.instance.CreatePool(asteroidObject, 10);
    }
    public void SpawnAsteroid(Vector2 _spawnPosition, int _startHealth)
    {
        Asteroid _asteroid = PoolManager.instance.ReuseObject(asteroidObject, _spawnPosition, Quaternion.identity).GetComponent<Asteroid>();
        _asteroid.Health = _startHealth;
        _asteroid.OnObjectReuse();

    }
    public static AsteroidSpawner Instance
    {
        get
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }


}
