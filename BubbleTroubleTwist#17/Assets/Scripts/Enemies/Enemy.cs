using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class Enemy : AbstractAvatarClass, IStats<EnemyData>
{
    public OnLevelUpdate onLevelUpdateHandler;
    private EnemyData enemyInput;

    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }

    public void SplitEnemy()
    {
        /*
        if (enemyInput == null) return;
        foreach (Transform point in enemyInput.splitPoints)
        {
            GameObject child = ObjectPooler.Instance.SpawnFromPool(enemyInput.splitChildName, point.position, Quaternion.identity);
            child.transform.localScale = new Vector3(child.transform.localScale.x / 2, child.transform.localScale.y / 2, child.transform.localScale.z / 2);
        }*/
        //LevelManager.Instance.UpdateLevel();
        //EventManager.Broadcast(EVENT.MyEvent2);
        EventManager.onUpdateCallerHandler();
        gameObject.SetActive(false);
    }

    public void SetStats(EnemyData data)
    {
        enemyInput = data;
    }

}
