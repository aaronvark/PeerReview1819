using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The poolmanager is generic and handles the object pooling and reusing of reusable prefabs.
/// </summary>
public class PoolManager : MonoBehaviour
{

    //Creates a list of object instances.
    Dictionary<int, PoolInstance<PoolObject>> poolDictionary = new Dictionary<int, PoolInstance<PoolObject>>();


    //An instance making sure only one of this object can exist in a scene or application? I dunno
    static PoolManager _instance;
    public static PoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Creates the pool of the prefabs on how big it should be, it loads them all immediatly
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="size"></param>
	public void CreatePool(GameObject prefab, int size, bool fixedSize = false)
    { 
        int key = prefab.GetInstanceID();
        if (!poolDictionary.ContainsKey(key))
        {
            poolDictionary.Add(key, new PoolInstance<PoolObject>(prefab, size, transform, fixedSize));
        }
    }

    /// <summary>
    /// Reuses the prefab from the object pool back into the field
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <returns></returns>
	public GameObject ReuseObject(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        int key = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(key))
        {
            return poolDictionary[key].ReuseObject(pos, rot);
        }
        return null;
    }


    /// <summary>
    /// Pool instance is a pool which holds a specific object in which it reuses.
    /// THere can be more than one pool.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PoolInstance<T> where T : IPoolable
    {
        public Queue<ObjectInstance<T>> objects;
        private GameObject group;
        private GameObject prefab;
        private bool fixedSize; 
        public PoolInstance(GameObject _prefab, int _size, Transform _transform, bool _fixedSize = false)
        {
            prefab = _prefab;
            fixedSize = _fixedSize;

            objects = new Queue<ObjectInstance<T>> { };

            group = new GameObject(_prefab.name + " pool");
            group.transform.parent = _transform;
            for (int i = 0; i < _size; i++)
            {
                CreateInstanceInPool();
            }
        }
        public ObjectInstance<T> CreateInstanceInPool()
        {
            ObjectInstance<T> newObject = new ObjectInstance<T>(Instantiate(prefab) as GameObject);
            objects.Enqueue(newObject);
            newObject.SetParent(group.transform);
            return newObject;
        }
        public GameObject ReuseObject(Vector3 pos, Quaternion rot)
        {
            ObjectInstance<T> obj = objects.Dequeue();
            objects.Enqueue(obj);

            //if pool is full, it adds a new object to the pool
            if (obj.gameObject.activeSelf)
            {
                if (fixedSize)
                {
                    return null;
                }
                obj = CreateInstanceInPool();
            }
            obj.Reuse(pos, rot);
            return obj.gameObject;
        }
    }


    /// <summary>
    /// The object instance is a gameobject that is held in the pool class, it is given a certain name and is made inactive in the group of the pool.
    /// </summary>
    public class ObjectInstance<T> where T : IPoolable
    {
        //the gameobject itself
        public GameObject gameObject;
        //its position
        Transform transform;
        //checks if it has pool object component in itself, prevents bugs.
        bool hasPoolComponent;
        //the poolobject script itself.
        T poolObjectScript;

        /// <summary>
        /// Creates the pool object and defines the variables
        /// Then it is placed into the pool group (this gameobject)
        /// </summary>
        /// <param name="objectInstance"></param>
		public ObjectInstance(GameObject objectInstance)
        {
            gameObject = objectInstance;
            transform = gameObject.transform;
            gameObject.SetActive(false);
            if (gameObject.GetComponent<T>() != null)
            {
                poolObjectScript = gameObject.GetComponent<T>();
            }
        }
        /// <summary>
        /// Reuses the object, placing it outside of the pool group and makes it active.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
		public void Reuse(Vector3 pos, Quaternion rot)
        {
            if (gameObject.GetComponent<PoolObject>())
            {
                poolObjectScript.OnObjectReuse();
            }
            transform.position = pos;
            transform.rotation = rot;
        }
        /// <summary>
        /// Makes the transofrm the parent of this gameobject.
        /// </summary>
        /// <param name="parent"></param>
		public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }
    }
}
