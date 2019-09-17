using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Scripts
    public ObjectPooler ObjectPoolerInstance { get; private set;}

    //Public Variables
    public float SpawnTime;

    // Start is called before the first frame update
    private void Start()
    {
        ObjectPoolerInstance = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
        InvokeRepeating("SpawnCube", 3f, SpawnTime);
        StartCoroutine(RepeatingCoroutine(1f, SpawnCube));

    }

    private void SpawnCube()
    {
        Vector3 position = new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y,transform.position.z +  Random.Range(-4f, 4f));
        ObjectPoolerInstance.SpawnFromPool("Box", position, Quaternion.identity);
    }

    private IEnumerator RepeatingCoroutine(float repeatTime, System.Action action)
    {
        action?.Invoke();
        yield return new WaitForSeconds(repeatTime);
        StartCoroutine(RepeatingCoroutine(repeatTime, action));
    }
}
