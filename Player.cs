using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;
    private Block currentBlock;
    public Color playerColor;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    //TODO: change controls to 8-directional + rotation
    public void Rotate(int direction)
    {
        if (currentBlock != null)
        {
            currentBlock.transform.Rotate(new Vector3(0, 0, direction * -1f));
        }
    }

    public void Move(bool fast)
    {
        if (currentBlock != null)
        {
            currentBlock.rigidB.velocity = -transform.position.normalized * (fast ? 2 : 1) * GameManager.Instance.blockSpeed;
        }
    }

    public void NewBlock()
    {
        GameObject _block = Instantiate(GameManager.Instance.blockPrefabs[Random.Range(0, GameManager.Instance.blockPrefabs.Length)]);
        _block.transform.position = transform.position * 0.9f;        
        currentBlock = _block.GetComponent<Block>();
        currentBlock.owner = this;
        GameManager.Instance.blocks.Add(currentBlock);
    }

    public void AddScore(int _addAmount)
    {

    }
}
