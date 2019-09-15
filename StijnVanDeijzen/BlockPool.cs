using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool
{
    //Efficient?

    private List<GameObject> pool;
    private GameObject block;
    private Transform root;

    public BlockPool(GameObject _block, int startSize)
    {
        block = _block;
        root = new GameObject().transform;
        root.name = "BlockPool";
        pool = new List<GameObject>();
        for (int i = 0; i < startSize; i++)
        {
            pool.Add(GameObject.Instantiate(block,root));
            pool[i].SetActive(false);
        }
    }

    public GameObject GetNext()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool[0];
            obj.SetActive(true);
            pool.RemoveAt(0); 
            return obj;
        }
        else
        {
            return GameObject.Instantiate(block, root);
        }
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

}