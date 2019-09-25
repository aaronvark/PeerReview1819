using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [SerializeField]
    private List<NestedPool> pool;

    [SerializeField]
    private GameObject[] prefabs;
    private int currentIndex = 0;

    [SerializeField]
    private int size;

    private int spawnIndex;

    private void Start()
    {
        pool = new List<NestedPool>(4);
        foreach (GameObject prefab in prefabs)
        {
            NestedPool _temp = new NestedPool();
            pool.Add(_temp);
        }

        // pool[currentIndex] = new List<GameObject>(size);
        ObjectPool(size);
    }

    public void ObjectPool(int size)
    {
        foreach (GameObject prefab in prefabs) {

            pool[spawnIndex].nesting = new List<GameObject>(size);
            for (int i = 0; i < size; ++i)
            {
                Spawn();
            }
            spawnIndex += 1;

        }
    }

    public GameObject GetNext(int _value, Vector3 _position, Quaternion _rotation)
    {
        GameObject obj = pool[_value].nesting[currentIndex];
        currentIndex = ++currentIndex % pool[_value].nesting.Count;
        if (obj != null)
        {
            obj.SetActive(true);
            // rotates obj items according to the pool
            obj.transform.position = _position;
            obj.transform.rotation = _rotation;
        }
        return obj;
    }

    private void Spawn()
    {
        GameObject obj = Object.Instantiate(prefabs[spawnIndex]);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool[spawnIndex].nesting.Add(obj);  
    }
}
[System.Serializable]
public class NestedPool
{
    public List<GameObject> nesting;
}