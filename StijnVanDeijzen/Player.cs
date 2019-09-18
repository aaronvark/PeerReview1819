using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int score = 0;
    public int energy = 0;
    private Block nextBlock;
    private Block currentBlock;
    private Block savedBlock;
    public Vector3 spawnPos;

    public Player(Vector3 _spawnPos)
    {
        spawnPos = _spawnPos;
    }

    public void ProcessInput(int _playerNumber)
    {
        currentBlock?.Move(PlayerInput.Get(_playerNumber, PlayerInput.InputType.HORIZONTAL), PlayerInput.Get(_playerNumber, PlayerInput.InputType.VERTICAL));
        currentBlock?.Rotate(PlayerInput.Get(_playerNumber, PlayerInput.InputType.ROTATE));
        if (PlayerInput.Get(_playerNumber, PlayerInput.InputType.SAVE) == 1) { SaveBlock(); }
    }

    private void SaveBlock()
    {
        //TODO: maybe UI class can move blocks to positions or at least give the position
        if (savedBlock == null)
        {
            savedBlock = currentBlock;
            savedBlock.transform.position = spawnPos * 1.5f + new Vector3(0, 5, 0); //Position part of UI
            savedBlock.SetFreeze(true);
            NewBlock();
        }
        else
        {
            //old block
            Block _temp = savedBlock;
            savedBlock = currentBlock;
            currentBlock.transform.position = spawnPos * 1.5f + new Vector3(0, 5, 0);//Position part of UI
            savedBlock.SetFreeze(true);
            //new block
            currentBlock = _temp;
            currentBlock.transform.position = spawnPos;
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
        currentBlock.transform.position = spawnPos;
        currentBlock = currentBlock.GetComponent<Block>();
        currentBlock.SetFreeze(false);
        currentBlock.owner = this;

        GameObject _newblock2 = BlockPool.GetNext();
        nextBlock = _newblock2.GetComponent<Block>();
        nextBlock.transform.position = spawnPos * 1.5f + new Vector3(0, -5, 0);//Position part of UI
        nextBlock.SetFreeze(true);

        AudioPlayer.Instance.PlaySound("SpawnBlock", 0.5f);
    }

    public void AddScore(int _addAmount)
    {
        score += _addAmount;
        energy = Mathf.Min( ++energy , 3);
    }
}
