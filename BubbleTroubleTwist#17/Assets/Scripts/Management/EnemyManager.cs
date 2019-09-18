using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;


public class EntityManager<T> where T : MonoBehaviour
{
    public List<T> entityManagers;
    public GameObject entityManagerGameObject;

    public void InitEntities(T entity, T entityStats)
    {

    }

    public void InitEntityManagers(T _entityManager)
    {
        foreach(T entyManager in entityManagers)
        {
            //entityManagerGameObject.AddComponent<entyManager>();
        }
    }
}

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
            GameObject enemyGameObject = ObjectPooler.Instance.SpawnFromPool(enemy, enemyStats.SpawnPoint.position, Quaternion.identity);
            //Get the interface of each player and give it its stats
            var component = enemyGameObject.GetComponent<IStats<EnemyData>>();
            component.SetStats(enemyStats);
        }
    }
}
