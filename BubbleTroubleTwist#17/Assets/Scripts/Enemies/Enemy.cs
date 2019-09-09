using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Enemy : AbstractAvatarClass, IStats<EnemyData>
{
    public OnLevelUpdate onLevelUpdateHandler;
    public EnemyData enemyInput;

    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }

    public override void Start()
    {
        base.Start();
        for(int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            enemyInput.splitPoints.Add(transform.GetChild(childIndex));
        }
    }

    public void SplitEnemy()
    {
        if (enemyInput == null) return;
        if (enemyInput.level < 3)
        {
            foreach (Transform point in enemyInput.splitPoints)
            {
                GameObject child = ObjectPooler.Instance.SpawnFromPool(enemyInput.splitChildName, point.position, Quaternion.identity);
                child.transform.localScale = new Vector3(child.transform.localScale.x / 2, child.transform.localScale.y / 2, child.transform.localScale.z / 2);
                child.GetComponent<Enemy>().enemyInput.level++;
            }
        }
        EventManager.onLevelUpdateHandler();
        gameObject.SetActive(false);
    }

    public void SetStats(EnemyData data)
    {
        enemyInput = data;
    }

}
