using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyData class to store foreach unique enemy its own data
/// </summary>
[System.Serializable]
public class EnemyData
{
    /// <summary>
    /// string for the splitting child objects we want to spawn
    /// </summary>
    public string splitChildName;

    /// <summary>
    /// sets the amount of speed where the enemy should be moving with
    /// </summary>
    public float speed;

    /// <summary>
    /// the level of the current enemy
    /// </summary>
    public int level;

    /// <summary>
    /// List of transforms for all split point spawns
    /// </summary>
    public List<Transform> splitPoints;

    /// <summary>
    /// current enemy spawnpoint 
    /// </summary>
    public Transform spawnPoint;

    /// <summary>
    /// sets the amount of points the player is able to earn from this enemy
    /// </summary>
    public int points;
}
