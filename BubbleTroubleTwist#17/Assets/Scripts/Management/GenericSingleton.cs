using UnityEngine;

/// <summary>
/// Generic singleton class for easy inheritance 
/// </summary>
/// <typeparam name="T">Component of type T</typeparam>
/// <typeparam name="A">Interface of type A</typeparam>
public class GenericSingleton<T, A> : MonoBehaviour where T : Component, A
{
    private static T instance;
    public static A Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
