using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockPool
{
    private static GameManager gameManager;

    private static List<GameObject> pool;
    private static GameObject block;
    private static Transform root;

    public static void Initialize(GameManager _gameManager)
    {
        gameManager = _gameManager;

        block = Resources.Load<GameObject>("Block");
        if (block == null)
        {
            Debug.LogError("Block Prefab not found in Resources");
        }

        root = new GameObject().transform;
        root.name = "BlockPool";
        pool = new List<GameObject>();

        for (int i = 0; i < 20; i++)
        {
            GameObject _newblock = GameObject.Instantiate(block, root);
            _newblock.GetComponent<Block>().gameManager = gameManager;
            pool.Add(_newblock);
            pool[i].SetActive(false);
        }
    }

    public static GameObject GetNext()
    {
        if (pool.Count > 0)
        {
            GameObject _obj = pool[0];
            _obj.SetActive(true);
            pool.RemoveAt(0);
            return _obj;
        }
        else
        {
            GameObject _newblock = GameObject.Instantiate(block, root);
            _newblock.GetComponent<Block>().gameManager = gameManager;
            return _newblock;
        }
    }

    public static void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }

}