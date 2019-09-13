using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyData class to store foreach unique enemy its own data
/// </summary>
[System.Serializable]
public class EnemyData
{
    public string splitChildName;
    public float speed;
    public int level;
    public List<Transform> splitPoints;
    public Transform spawnPoint;
    public int points;
}
