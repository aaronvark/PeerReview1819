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
    /// Enemy his to low spot to check if the enemy is to low
    /// </summary>
    public float toLow = 1f;

    public bool checking = true;
    /// <summary>
    /// Overriden start method
    /// </summary>
    public override void Start()
    {
        ///First we call the base
        base.Start();
        try
        {
            enemyInput.Equals(enemyInput);
        }
        catch
        {
            enemyInput = new EnemyData();
        }

        /// We add all children to the splitPoints list stored in the enemyInput data
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            enemyInput.splitPoints.Add(transform.GetChild(childIndex));
        }

        /// We give a random force so every enemy acts different
        rBody.AddForce(new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5) * 10f), ForceMode.Impulse);

        ///We always need points
        if (enemyInput.points == 0)
            enemyInput.points = 10;
    }

    private void FixedUpdate()
    {
        
        if(transform.position.y < toLow && checking)
        {
            StartCoroutine(ToLowChecking(toLow));
        }
    }

    private IEnumerator ToLowChecking(float _low)
    {
        checking = false;
        float time = 0;
        while(transform.position.y < _low)
        {
            time++;
            yield return new WaitForEndOfFrame();
        }

        if (time > _low)
        {
            rBody.AddForce(Vector3.up * (Speed), ForceMode.Impulse);
            time = 0;
            yield return new WaitForSeconds(_low + 5);
            checking = true;
        }
        else
        {
            yield return new WaitForSeconds(_low + 5);
            checking = true;
        }
    }
    private bool ToLowCheck(float _low)
    {
        int time = 0;
        while(transform.position.y < _low)
        {
            time++;
            if (time > 5)
                return true;
            else
                return false;
        }
        return false;
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

}
