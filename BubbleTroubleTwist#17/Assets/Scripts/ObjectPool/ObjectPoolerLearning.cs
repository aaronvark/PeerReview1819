using System.Collections.Generic;
using UnityEngine;
using System;
using Bas.Interfaces;
using UnityEngine.SceneManagement;

public class ObjectPoolerLearning : GenericSingleton<ObjectPoolerLearning, IGenericPooler>, IGenericPooler
{
    private Dictionary<Type, Queue<UnityEngine.Object>> poolDictionary = new Dictionary<Type, Queue<UnityEngine.Object>>();

    private const int startAmount = 30;

    public override void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        base.OnLevelFinishedLoading(scene, mode);
        InitPools();
    }

    private void InitPools()
    {
        //Load All poolable objects found in the Resources/PoolableObjects
        UnityEngine.Object[] allPoolableObjects = Resources.LoadAll("PoolableObjects", typeof(IPoolable));
        foreach (UnityEngine.Object prototype in allPoolableObjects)
        {
            if(!poolDictionary.ContainsKey(prototype.GetType()))
                poolDictionary.Add(prototype.GetType(), new Queue<UnityEngine.Object>());

            for (int i = 0; i < startAmount; i++)
            {
                poolDictionary[prototype.GetType()].Enqueue(MonoBehaviour.Instantiate(prototype));
            }
        }
    }

    public T GetObjectFromPool<T>() where T : UnityEngine.Object
    {
        if (poolDictionary.ContainsKey(typeof(T)))
        {
            T objectToSpawn = poolDictionary[typeof(T)].Dequeue() as T;

            poolDictionary[typeof(T)].Enqueue(objectToSpawn);

            return objectToSpawn;
        }
        return default;
    }

    public GameObject SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T : UnityEngine.Object
    {
        if (!poolDictionary.ContainsKey(typeof(T)))
        {
            Debug.LogWarning("Pool with type: " + typeof(T) + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[typeof(T)].Dequeue() as GameObject;
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[typeof(T)].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public static UnityEngine.Object[] FindObjectsOfTypeByName(string aClassName)
    {
        var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            var types = assemblies[i].GetTypes();
            for (int n = 0; n < types.Length; n++)
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(types[n]) && aClassName == types[n].Name)
                    return UnityEngine.Object.FindObjectsOfType(types[n]);
            }
        }
        return new UnityEngine.Object[0];
    }
}