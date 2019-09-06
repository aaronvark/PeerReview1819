using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO scriptable object maken?

[System.Serializable]
[CreateAssetMenu(fileName = "Pool", menuName = "ObjectPooling/PoolableObject", order = 1)]
public class Pool : ScriptableObject
{
     public string poolName;
     public GameObject prefab;
     public int size;
}

