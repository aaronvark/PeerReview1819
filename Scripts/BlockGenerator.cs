using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockCollection;

    public void Start()
    {
        GenerateNewBlock<Block>();
    }

    /// <summary>
    /// Generating the new block.
    /// </summary>
    public void GenerateNewBlock<T>() where T : Block
    {
        T block = Pool.Instance.GetObjectFromPool<T>();

        block.gameObject.SetActive(true);
        block._onHitGround += BlockHitGround;
    }

    private void BlockHitGround(Block block)
    {
        block._onHitGround -= BlockHitGround;

        GenerateNewBlock<Block>();
    }
}
