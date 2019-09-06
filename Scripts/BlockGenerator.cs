using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _blockCollection;

    public void Start()
    {
        GenerateNewBlock();
    }

    /// <summary>
    /// Generating the new block.
    /// </summary>
    public void GenerateNewBlock()
    {
        int randomNumber = Random.Range(0, _blockCollection.Length);
        GameObject block = _blockCollection[randomNumber];

        Instantiate<GameObject>(block).GetComponent<Block>()._onHitGround += BlockHitGround;

    }

    private void BlockHitGround(Block block)
    {
        block._onHitGround -= BlockHitGround;
        GenerateNewBlock();
    }
}
