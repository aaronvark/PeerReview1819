using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level 
{
    public int enemyAmounts;
    public int enemiesAlive;
    public Vector3 spawnRangeMax;
    public Vector3 spawnRangeMin;
    public bool done;
    public Vector3 nextLevelPosition;
    public Vector3 currentLevelPostion;
    public float currentCameraX;
    public GameObject levelPrefab;
}
