using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Scripts
    ObjectPooler objectPooler;

    //Public Variables
    public float SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        InvokeRepeating("SpawnCube", 3f, SpawnTime);
    }

    void SpawnCube()
    {
        Vector3 position = new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y,transform.position.z +  Random.Range(-4f, 4f));
        objectPooler.SpawnFromPool("Box", position, Quaternion.identity);
    }
}
