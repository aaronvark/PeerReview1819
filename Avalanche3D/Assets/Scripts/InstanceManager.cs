using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager<T> : MonoBehaviour
{
    public static Dictionary<string, T> Instances = new Dictionary<string, T>();

    public static void CreateInstance(string name, T script)
    {
        Instances.Add(name, script);
    }

    public static T GetInstance(string name)
    {
        return Instances[name];
    }
}
