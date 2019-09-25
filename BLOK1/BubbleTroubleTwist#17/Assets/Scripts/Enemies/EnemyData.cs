using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EnemyData class to store foreach unique enemy its own data
/// </summary>
[System.Serializable]
public class EnemyData
{
    /// <summary>
    /// Gameobject for the splitting child objects we want to spawn
    /// </summary>
    [SerializeField] private GameObject splitChildPrefab;
    public GameObject SplitChildPrefab { get => splitChildPrefab; set => splitChildPrefab = value; }

    /// <summary>
    /// sets the amount of speed where the enemy should be moving with
    /// </summary>
    public float Speed { get; set; }

    /// <summary>
    /// the level of the current enemy
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// List of transforms for all split point spawns
    /// </summary>
    public List<Transform> SplitPoints { get; set; } = new List<Transform>();

    /// <summary>
    /// current enemy spawnpoint 
    /// </summary>
    public Transform SpawnPoint { get; set; }

    /// <summary>
    /// sets the amount of points the player is able to earn from this enemy
    /// </summary>
    [SerializeField] private int points;
    public int Points { get => points; set => points = value; }
}
