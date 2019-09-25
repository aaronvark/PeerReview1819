using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    [SerializeField]
    private List<Transform> child;

    /// <summary>
    /// Getting the component from the pool.
    /// </summary>
    /// <returns>component T</returns>
    public T GetObjectFromPool<T>() where T : MonoBehaviour
    {
        int random = Random.Range(0, child.Count - 1);
        T component = child[random].GetComponent<T>();
        if (component != null)
        {
            child.Remove(component.transform);
            return component;
        }

        return default(T);
    }

    public void ReturnObjectToPool<T>(T component) where T : MonoBehaviour
    {
        component.gameObject.SetActive(false);
        child.Add(component.transform); }
    


}
