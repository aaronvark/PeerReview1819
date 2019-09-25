using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField] private int enemyAmounts;
    public int EnemyAmounts { get => enemyAmounts; set => enemyAmounts = value; }
    [SerializeField] private int enemiesAlive;
    public int EnemiesAlive { get => enemiesAlive; set => enemiesAlive = value; }
    [SerializeField] private Vector3 spawnRangeMax;
    public Vector3 SpawnRangeMax { get => spawnRangeMax; set => spawnRangeMax = value; }
    [SerializeField] private Vector3 spawnRangeMin;
    public Vector3 SpawnRangeMin { get => spawnRangeMin; set => spawnRangeMin = value; }
    [SerializeField] private bool done;
    public bool Done { get => done; set => done = value; }
    [SerializeField] private Vector3 nextLevelPosition;
    public Vector3 NextLevelPosition { get => nextLevelPosition; set => nextLevelPosition = value; }
    [SerializeField] private Vector3 currentLevelPostion;
    public Vector3 CurrentLevelPostion { get => currentLevelPostion; set => currentLevelPostion = value; }
    [SerializeField] private float currentCameraX;
    public float CurrentCameraX { get => currentCameraX; set => currentCameraX = value; }
    [SerializeField] private GameObject levelPrefab;
    public GameObject LevelPrefab { get => levelPrefab; set => levelPrefab = value; }


}
