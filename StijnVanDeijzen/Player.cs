using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score;
    private Block currentBlock;
    public Block GetBlock() { return currentBlock; }
    public Color playerColor;   

    public void NewBlock()
    {
        GameObject[] _blockPrefabs = GameManager.Instance.GetBlocksPrefabs();
        GameObject _block = Instantiate(_blockPrefabs[Random.Range(0, _blockPrefabs.Length)]);
        _block.transform.position = transform.position * 0.9f;        
        currentBlock = _block.GetComponent<Block>();
        currentBlock.owner = this;
    }

    public void AddScore(int _addAmount)
    {
        score += _addAmount;
    }
}
