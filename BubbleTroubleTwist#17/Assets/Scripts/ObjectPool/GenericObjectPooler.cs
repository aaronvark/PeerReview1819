using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPooler
{
    void Return(PoolableBehaviour c);
}

public class GenericObjectPooler<T> : IObjectPooler where T : PoolableBehaviour
{
    private GameObject prefab;
    private int size = 0;

    private Stack<T> objectStack;
    private GameObject root;

    public GenericObjectPooler(GameObject _prefab, int _size)
    {
        if (_prefab.GetComponent<T>() == null)
        {
            throw new System.NullReferenceException("Prefab does not contain component of type " + typeof(T).ToString());
        }

        root = new GameObject("__pool<" + typeof(T).ToString() + ">");

        prefab = _prefab;
        size = _size;

        objectStack = new Stack<T>(_size);
        for (int i = 0; i < _size; i++)
        {
            Spawn();
        }
    }

    public T GetNext()
    {
        // strategy 1: loop through fixed size list with index
        //   ignores if objects have "lived out their use"
        //T component = objects[index];
        //index = index++ % objects.Count;

        //strategy 2: pop a stack, if it's empty, add more
        if (objectStack.Count == 0)
        {
            Spawn();
        }
        T component = objectStack.Pop();

        //component.gameObject.SetActive(true);
        return component;
    }

    public void Return(PoolableBehaviour c)
    {
        c.gameObject.SetActive(false);
        objectStack.Push((T)c);
    }

    private void Spawn()
    {
        T c = Object.Instantiate(prefab).GetComponent<T>();
        // objects.Add(c);
        c.owner = this;
        c.transform.parent = root.transform;
        objectStack.Push(c);
        c.gameObject.SetActive(false);
    }
}

public abstract class PoolableBehaviour : MonoBehaviour
{
    public IObjectPooler owner;
    protected void Recycle()
    {
        owner.Return(this);
    }
}
