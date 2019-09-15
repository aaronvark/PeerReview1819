using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score = 0;
    public int energy = 0;
    private Block nextBlock;
    private Block currentBlock;
    private Block savedBlock;
    public Block GetBlock() { return currentBlock; }
    public Color playerColor;

    public void ProcessInput(float[] input)
    {
        currentBlock?.Move(input[0], input[1]);
        currentBlock?.Rotate(input[2]);
        if (input[3] == 1) { SaveBlock(); }
    }

    public void SaveBlock()
    {
        //TODO : freeze block when outside game
        if (savedBlock == null)
        {
            savedBlock = currentBlock;
            savedBlock.transform.position = transform.position * 1.2f + new Vector3(0, 5, 0);
            NewBlock();
        }
        else
        {
            //old block
            Block temp = savedBlock;
            savedBlock = currentBlock;
            currentBlock.transform.position = transform.position * 1.2f + new Vector3(0, 5, 0);
            //new block
            currentBlock = temp;
            currentBlock.transform.position = transform.position * 0.8f;
        }
    }

    public void NewBlock()
    {
        if (nextBlock != null)
        {
            currentBlock = nextBlock;
        }
        else
        {
            GameObject _newblock = GameManager.Instance.blockPool.GetNext();
            currentBlock = _newblock.GetComponent<Block>();
        }
        currentBlock.transform.position = transform.position * 0.8f;
        currentBlock = currentBlock.GetComponent<Block>();
        currentBlock.owner = this;

        GameObject _newblock2 = GameManager.Instance.blockPool.GetNext();
        nextBlock = _newblock2.GetComponent<Block>();
        nextBlock.transform.position = transform.position * 1.2f + new Vector3(0, -5, 0);
    }

    public void AddScore(int _addAmount)
    {
        score += _addAmount;
        energy = Mathf.Min( ++energy , 3);
    }
}
