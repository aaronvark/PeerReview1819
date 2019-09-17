using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class MyScriptableObjectClass : ScriptableObject, IPoolable
{
    public System.Type instanceType;
    public string tag;
    public GameObject prefab;
    public int size;
}
