using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractAvatarClass
{
    public string splitChildName;
    public Transform[] spawnPoints;

    private void Bounce()
    {

    }

    public void SplitEnemy()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject child = objectPooler.SpawnFromPool(splitChildName, point.position, Quaternion.identity);
            child.transform.localScale = new Vector3(child.transform.localScale.x / 2, child.transform.localScale.y / 2, child.transform.localScale.z / 2);
        }
        gameObject.SetActive(false);
    }

    
}
