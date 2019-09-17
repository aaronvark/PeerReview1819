using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockPool
{
    private static List<GameObject> pool;
    private static GameObject block;
    private static Transform root;      

    static BlockPool()
    {

        block = Resources.Load<GameObject>("Block");
        if(block == null)
        {
            Debug.LogError("Block Prefab not found in Resources");
        }

        root = new GameObject().transform;
        root.name = "BlockPool";
        pool = new List<GameObject>();

        for (int i = 0; i < 20; i++)
        {
            pool.Add(GameObject.Instantiate(block,root));
            pool[i].SetActive(false);
        }
    }

    public static GameObject GetNext()
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

    public static void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

}