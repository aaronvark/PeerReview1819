using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/*
public class EntityManager<T> : MonoBehaviour
{
    public List<T> entitiesStats;
    public GameObject entityGameObject;

    public void InitEntities()
    {
        foreach (T entityStats in entitiesStats)
        {
            //Spawn a player foreach player in the PlayerData list
            GameObject enemyGameObject = ObjectPooler.Instance.SpawnFromPool(entityGameObject.name, entityStats.spawnPoint.position, Quaternion.identity);
            //Get the interface of each player and give it its stats
            var component = enemyGameObject.GetComponent<IStats<EnemyData>>();
            component.SetStats(entityStats);
        }
    }
}*/

public class EnemyManager : MonoBehaviour
{
    public List<EnemyData> enemysStats;
    public GameObject enemy;

    private void Start()
    {
        if (enemysStats == null) return;
        InitEnemies();
    }

    public void InitEnemies()
    {
        foreach (EnemyData enemyStats in enemysStats)
        {
            //Spawn a player foreach player in the PlayerData list
            GameObject enemyGameObject = ObjectPooler.Instance.SpawnFromPool(enemy.name, enemyStats.spawnPoint.position, Quaternion.identity);
            //Get the interface of each player and give it its stats
            var component = enemyGameObject.GetComponent<IStats<EnemyData>>();
            component.SetStats(enemyStats);
        }
    }
}
