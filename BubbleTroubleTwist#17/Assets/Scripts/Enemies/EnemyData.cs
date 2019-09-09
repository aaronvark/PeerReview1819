using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string splitChildName;
    public float speed;
    public int level;
    public List<Transform> splitPoints;
    public Transform spawnPoint;
}
