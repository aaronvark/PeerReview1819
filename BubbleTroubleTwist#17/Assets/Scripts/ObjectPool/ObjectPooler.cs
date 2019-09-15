using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using UnityEngine.SceneManagement;

public class GenericObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    // Prefab for this pool. The prefab must have a component of type T on it.
    public T m_prefab;

    // Size of this object pool
    public int m_size;

    // The list of free and used objects for tracking.
    // We use the generic collections so we can give them our type T.
    private List<T> m_freeList;
    private List<T> m_usedList;

    public void Awake()
    {
        m_freeList = new List<T>(m_size);
        m_usedList = new List<T>(m_size);

        // Instantiate the pooled objects and disable them.
        for (var i = 0; i < m_size; i++)
        {
            var pooledObject = Instantiate(m_prefab, transform);
            pooledObject.gameObject.SetActive(false);
            m_freeList.Add(pooledObject);
        }
    }

    public T Get()
    {
        var numFree = m_freeList.Count;
        if (numFree == 0)
            return null;

        // Pull an object from the end of the free list.
        var pooledObject = m_freeList[numFree - 1];
        m_freeList.RemoveAt(numFree - 1);
        m_usedList.Add(pooledObject);
        pooledObject.gameObject.SetActive(true);
        return pooledObject;
    }

    // Returns an object to the pool. The object must have been created
    // by this ObjectPool.
    public void ReturnObject(T pooledObject)
    {
        Debug.Assert(m_usedList.Contains(pooledObject));

        // Put the pooled object back in the free list.
        m_usedList.Remove(pooledObject);
        m_freeList.Add(pooledObject);

        // Reparent the pooled object to us, and disable it.
        var pooledObjectTransform = pooledObject.transform;
        pooledObjectTransform.parent = transform;
        pooledObjectTransform.localPosition = Vector3.zero;
        pooledObject.gameObject.SetActive(false);
    }
}

public class ObjectPooler : GenericSingleton<ObjectPooler, IPooler>, IPooler
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public override void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        base.OnLevelFinishedLoading(scene, mode);
        InitPools();
    }

    // Use this for initialization
    public void Awake()
    {
        InitPools();
    }

    private void InitPools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            pool.tag = pool.prefab.name;
            GameObject containerObject = new GameObject(pool.tag);
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.prefab, containerObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }
}
