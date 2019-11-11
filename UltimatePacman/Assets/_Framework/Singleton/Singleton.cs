using UnityEngine;

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
#endif

#if ODIN_INSPECTOR
using MonoBehaviour = Sirenix.OdinInspector.SerializedMonoBehaviour;
#endif

/// <summary>
/// Generic Implementation of a Singleton MonoBehaviour.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Returns the instance of this singleton.
    /// </summary>
    public static T Instance => instance ?? (instance = FindObjectOfType<T>());

    private static T instance = null;

    protected Singleton() { }

    private void Awake()
    {
        T thisInstance = transform.GetComponent<T>();
        if (instance == null)
        {
            instance = thisInstance;
        }
        else if (!instance.Equals(thisInstance))
        {
            Destroy(thisInstance);
            if(transform.GetComponents<Singleton<MonoBehaviour>>().Length == 0)
                Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_EDITOR
    private void Reset()
    {
        GameObject gameObject = this.gameObject;

        // Check if this scene already contains an instance of the Singleton
        bool sceneHasInstance = gameObject.scene.GetRootGameObjects()
            .Any(go => go.GetComponentInChildren<T>(true) 
                && (!go.GetComponentInChildren<T>(true).gameObject.Equals(gameObject)
                    || gameObject.GetComponents<T>().Length > 1)
            );

        // If the scene already contains an instance, remove THIS instance and all its added RequiredComponents
        if (sceneHasInstance)
        {
            DestroyImmediate(this);

            List<Type> requiredComponentsType = new List<Type>();
            FindRequiredComponentsOfType(typeof(T));
            requiredComponentsType.Reverse();

            Stack<Component> components = new Stack<Component>(gameObject.GetComponents<Component>());

            foreach (var type in requiredComponentsType)
            {
                if (components.Pop().GetType() == type)
                {
                    Undo.DestroyObjectImmediate(gameObject.GetComponent(type));
                    Undo.IncrementCurrentGroup();
                }
                else break;
            }

            void FindRequiredComponentsOfType(Type type)
            {
                if (type == null)
                    return;
                
                MemberInfo memberInfo = type;
                RequireComponent[] requiredComponentsOfType =
                    Attribute.GetCustomAttributes(memberInfo, typeof(RequireComponent), true)
                    as RequireComponent[];

                for (int i = 0; i < requiredComponentsOfType.Length; i++)
                {
                    RequireComponent rc = requiredComponentsOfType[i];

                    HandleType(rc.m_Type0);
                    HandleType(rc.m_Type1);
                    HandleType(rc.m_Type2);
                }

                void HandleType(Type rcType)
                {
                    if (rcType == null || rcType == typeof(Transform))
                        return;

                    if (!requiredComponentsType.Contains(rcType))
                    {
                        FindRequiredComponentsOfType(rcType);
                        requiredComponentsType.Add(rcType);
                    }
                }
            }
        }
    }
#endif
}
