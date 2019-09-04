using UnityEngine;

#if UNITY_EDITOR
using System;
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
public class Singleton<T> : MonoBehaviour, ISerializationCallbackReceiver where T : MonoBehaviour
{
    /// <summary>
    /// Returns the instance of this singleton.
    /// </summary>
    public static T instance;

    [HideInInspector] [SerializeField] private T cashedInstance = null;

    [Header("Singleton")]
    [SerializeField]
    private bool dontDestroyOnLoad = true;

    private void Awake()
    {
        T thisIstance = transform.GetComponent<T>();
        if (instance == null) 
        {
            instance = thisIstance;
        }

        if (!instance.Equals(thisIstance)) 
        {
            Destroy(gameObject);
        }
        else if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        cashedInstance = instance;
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        instance = cashedInstance;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        if (instance == null || cashedInstance != null)
        {
            instance = transform.GetComponent<T>();
        }
        else
        {
            GameObject gameObject = this.gameObject;
            DestroyImmediate(this);

            MemberInfo memberInfo = instance.GetType(); //monoInstanceCaller.GetType();
            RequireComponent[] requiredComponentsAtts = Attribute.GetCustomAttributes(
                memberInfo, typeof(RequireComponent), true) as RequireComponent[];
            Array.Reverse(requiredComponentsAtts);
            Component[] components = gameObject.GetComponents<Component>();

            foreach (RequireComponent rc in requiredComponentsAtts)
            {
                if (components[components.Length - 1].GetType() == rc.m_Type0)
                {
                    Array.Resize(ref components, components.Length - 1);
                    Undo.DestroyObjectImmediate(gameObject.GetComponent(rc.m_Type0));
                    Undo.IncrementCurrentGroup();
                }
            }
        }
    }
#endif
}
