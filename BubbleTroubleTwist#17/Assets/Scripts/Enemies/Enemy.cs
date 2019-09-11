using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

/// <summary>
/// Enemy base class, inherits from abstract avatar base and uses IStats interface to regulate its entity data
/// </summary>
public class Enemy : AbstractAvatarClass, IStats<EnemyData>
{
    /// <summary>
    /// Input of this enemy instance
    /// </summary>
    public EnemyData enemyInput;
    /// <summary>
    /// Maximum level the enemy and his childs should be able to reach
    /// </summary>
    public int MaxLevel = 2;

    /// <summary>
    /// Overriden start method
    /// </summary>
    public override void Start()
    {
        ///First we call the base
        base.Start();
        /// We add all children to the splitPoints list stored in the enemyInput data
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            enemyInput.splitPoints.Add(transform.GetChild(childIndex));
        }
    }

    /// <summary>
    /// Handles the activations and regulation of the children 
    /// </summary>
    public void SplitEnemy()
    {
        if (enemyInput == null) return;
        if (enemyInput.level < MaxLevel)
        {
            foreach (Transform point in enemyInput.splitPoints)
            {
                GameObject child = ObjectPooler.Instance.SpawnFromPool(enemyInput.splitChildName, point.position, Quaternion.identity);
                child.transform.localScale = new Vector3(child.transform.localScale.x / 2, child.transform.localScale.y / 2, child.transform.localScale.z / 2);
                enemyInput.level++;
                child.GetComponent<Enemy>().enemyInput.level = enemyInput.level;            
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
        EventManager.OnLevelUpdateHandler();
    }

    /// <summary>
    /// Implemented SetStats from the interface, here we set the enemy input
    /// </summary>
    /// <param name="data"></param>
    public void SetStats(EnemyData data)
    {
        enemyInput = data;
    }


    /// <summary>
    /// When this enemy gets destoryed we need to unsubscribe 
    /// </summary>
    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }

}
