using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    public static Toolbox Instance;
    private Dictionary<string, Component> Components = new Dictionary<string, Component>();

    
    void Awake()
    {
        Instance = this;
    }
    
    public void AddReference(string Name, Component component)
    {
        if (Components.ContainsKey(Name))
        {
            Debug.Log("Component with name: " + Name + " is already in toolbox.");
        }
        else
        {
            Components.Add(Name, component);
        }
    }

    public void RemoveReference(string Name, Component component)
    {
        if (Components.ContainsKey(Name))
        {
            Components.Remove(Name);
        }
        else
        {
            Debug.Log("Component with name: " + Name + " is not in toolbox.");
        }

    }

    public Component GetReference(string Name)
    {
        if(Components.ContainsKey(Name))
        {
            Component component = Components[Name];
            return component;
        }
        else
        {
            Debug.Log("Component with name: " + Name + " has not been found.");
            return null;
        }
    }
}
