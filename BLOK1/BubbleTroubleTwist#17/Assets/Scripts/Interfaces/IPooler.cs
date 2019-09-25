using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bas.Interfaces
{
    public interface IPooler
    {
        GameObject SpawnFromPool(Vector3 position, Quaternion rotation);
        GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation);
    }

    public interface IGenericPooler
    {
        MonoBehaviour GetObjectFromPool<T>() where T : MonoBehaviour;
        MonoBehaviour SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T : MonoBehaviour;
    }

    public interface IScriptableObjectPooler
    {
        //GameObject SpawnFromPool(MyScriptableObjectClass tag, Vector3 position, Quaternion rotation);
        GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}
