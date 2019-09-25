using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class MyScriptableObjectClass : ScriptableObject
{
    [SerializeField] private MonoBehaviour prefab;
    public MonoBehaviour Prefab
    {
        get
        {
            return prefab;
        }
        set
        {
            prefab = value;
        }
    }

    [SerializeField] private int size;
    public int Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
        }
    }
}
