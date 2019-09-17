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
    //public Color playerColor; for highlights, maybe use for graphics

    public void ProcessInput(float[] input)
    {
        currentBlock?.Move(input[0], input[1]);
        currentBlock?.Rotate(input[2]);
        if (input[3] == 1) { SaveBlock(); }
    }

    private void SaveBlock()
    {
        //DISCUSS: maybe UI class can move blocks to positions
        if (savedBlock == null)
        {
            savedBlock = currentBlock;
            savedBlock.transform.position = transform.position * 1.2f + new Vector3(0, 5, 0);
            savedBlock.SetFreeze(true);
            NewBlock();
        }
        else
        {
            //old block
            Block _temp = savedBlock;
            savedBlock = currentBlock;
            currentBlock.transform.position = transform.position * 1.2f + new Vector3(0, 5, 0);
            savedBlock.SetFreeze(true);
            //new block
            currentBlock = _temp;
            currentBlock.transform.position = transform.position * 0.8f;
            savedBlock.SetFreeze(false);
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
            GameObject _newblock = BlockPool.GetNext();
            currentBlock = _newblock.GetComponent<Block>();
        }
        currentBlock.transform.position = transform.position * 0.8f;
        currentBlock = currentBlock.GetComponent<Block>();
        currentBlock.SetFreeze(false);
        currentBlock.owner = this;

        GameObject _newblock2 = BlockPool.GetNext();
        nextBlock = _newblock2.GetComponent<Block>();
        nextBlock.transform.position = transform.position * 1.2f + new Vector3(0, -5, 0);
        nextBlock.SetFreeze(true);
    }

    public void AddScore(int _addAmount)
    {
        score += _addAmount;
        energy = Mathf.Min( ++energy , 3);
    }
}
