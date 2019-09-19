using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of asteroids and where to place them.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroidObject;
    private static WaveManager instance;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject spawnParticle;
    [SerializeField]
    private GameObject asteroidDestroyParticle;

    private Character character;

    private List<Asteroid> asteroids;

    private int wave = 1;
    private float timeBetweenSpawning = .5f;
    private float timeBetweenWave = .5f;

    public delegate void OnAsteroidDestroy(Asteroid asteorid);
    public static OnAsteroidDestroy onAsteroidDestroy;

    public delegate void OnStartWave(int wave);
    public static OnStartWave onWaveStart;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        PoolManager.instance.CreatePool(asteroidObject, 10);
        PoolManager.instance.CreatePool(spawnParticle, 3);
        PoolManager.instance.CreatePool(asteroidDestroyParticle, 3);
        asteroids = new List<Asteroid> { };
    }

    public void StartWave()
    {
        StartCoroutine(SpawningWave());
    }
    private IEnumerator SpawningWave()
    {
        WaveManager.onWaveStart?.Invoke(wave);

        //yield return new WaitForSeconds(timeBetweenWave);
        int _amount = wave;
        int _index = 0;
        while (_index < _amount)
        {
            int health = Random.Range(1, wave);
            SpawnAsteroid(FurthestPointFormPlayer().position, health, true);
            _index++;
            yield return new WaitForSeconds(timeBetweenSpawning);
        }
    }
    private void OnEnable()
    {
        WaveManager.onAsteroidDestroy += CheckAsteroids;
    }
    private void OnDisable()
    {
        WaveManager.onAsteroidDestroy -= CheckAsteroids;
    }
    public void NextWave()
    {
        wave++;
        StartWave();
    }

    public void CheckAsteroids(Asteroid asteroid)
    {
        PoolManager.instance.ReuseObject(asteroidDestroyParticle, asteroid.transform.position, asteroidDestroyParticle.transform.rotation);

        asteroids.Remove(asteroid);
        if (asteroids.Count == 0)
        {
            NextWave();
        }
        Debug.Log("check");
    }
    public void SpawnAsteroid(Vector2 _spawnPosition, int _startHealth, bool _withParticle = false)
    {
        Asteroid _asteroid = PoolManager.instance.ReuseObject(asteroidObject, _spawnPosition, Quaternion.identity).GetComponent<Asteroid>();
        _asteroid.Health = _startHealth;
        _asteroid.OnObjectReuse();
        asteroids.Add(_asteroid);
        if (_withParticle)
        {
            PoolManager.instance.ReuseObject(spawnParticle, _asteroid.transform.position, spawnParticle.transform.rotation);
        }

    }
    public Transform FurthestPointFormPlayer()
    {
        Transform _result = spawnPoints[0];
        float _maxdistance = 0;
        foreach (Transform _point in spawnPoints)
        {
            float cDistance = Vector2.Distance(_point.position, character.transform.position);
            if (_maxdistance < cDistance)
            {
                _maxdistance = cDistance;
                _result = _point;
            }
        }
        return _result;
    }
    public Character Character
    {
        set { character = value; }
    }
    public int Wave
    {
        get { return wave; }
    }
    public static WaveManager Instance
    {
        get
        {
            return instance;
        }
    }


}
