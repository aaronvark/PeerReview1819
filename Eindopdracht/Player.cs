using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;
    private Block currentBlock;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Rotate(int direction)
    {
        currentBlock.transform.Rotate(new Vector3(0,0,direction * -1f));
    }

    public void Move(bool fast)
    {
        currentBlock.rigidB.velocity = -transform.position.normalized * (fast ? 2 : 1) * GameManager.Instance.blockSpeed;
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
