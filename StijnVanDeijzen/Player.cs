using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score;
    private Block currentBlock;
    private Block savedBlock;
    public Block GetBlock() { return currentBlock; }
    public Color playerColor;

    public void SaveBlock()
    {
        if(savedBlock == null)
        {
            savedBlock = currentBlock;
            savedBlock.gameObject.SetActive(false);
            NewBlock();
        }
        else
        {
            Block temp = savedBlock;
            savedBlock = currentBlock;
            savedBlock.gameObject.SetActive(false);
            currentBlock = temp;
            currentBlock.gameObject.SetActive(true);
            currentBlock.transform.position = transform.position * 0.8f;
        }
    }

    public void NewBlock()
    {
        GameObject _block = GameManager.Instance.blockPool.GetNext();
        _block.transform.position = transform.position * 0.8f;
        currentBlock = _block.GetComponent<Block>();
        currentBlock.owner = this;
    }

    public void AddScore(int _addAmount)
    {
        score += _addAmount;
    }
}
