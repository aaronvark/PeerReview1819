using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Bas.Interfaces
{
    public interface IPooler
    {
        GameObject SpawnFromPool(Vector3 position, Quaternion rotation);
        GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation);
    }

    public interface IGenericPooler
    {
        T GetObjectFromPool<T>() where T : UnityEngine.Object;
        GameObject SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T : UnityEngine.Object;
    }

    public interface IScriptableObjectPooler
    {
        //GameObject SpawnFromPool(MyScriptableObjectClass tag, Vector3 position, Quaternion rotation);
        GameObject SpawnFromPool(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}
