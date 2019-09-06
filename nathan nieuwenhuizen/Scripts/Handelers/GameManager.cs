using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AsteroidSpawner asteroidSpawner;
    private static GameManager instance;
    public static GameManager Instance
    {
        get {
            return instance;
        }
        set {
            Instance = value;
        }
    }

    void Start()
    {
        instance = this;
        asteroidSpawner.SpawnAsteroid(asteroidSpawner.transform.position, 3);
    }
}
