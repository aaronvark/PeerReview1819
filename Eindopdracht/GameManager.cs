using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public float blockSpeed = 1;
    public float middleForce = 1;
    public float middleRotateSpeed = 1;

    private List<Block> blocks;
    public List<Block> GetBlocks() { return blocks; }
    public void AddBlock(Block _block) { blocks.Add(_block); }
    public void RemoveBlock(Block _block) { blocks.Remove(_block); }

    private GameObject[] blockPrefabs;
    public GameObject[] GetBlocksPrefabs() { return blockPrefabs; }

    //TODO: other way without player references?
    public Player[] players = new Player[2];

    public event System.Action GetInput;
    public event System.Action UpdateMiddle;

    private PlayerInput playerInput;
    private Middle middle;

    private void Awake()
    {
        if ( instance != null && instance != this){ Destroy(this.gameObject); }
        else{ instance = this; }
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        middle = FindObjectOfType<Middle>();

        GetInput += playerInput.GetInput;
        UpdateMiddle += middle.UpdateMiddle;

        players[0].NewBlock();
        players[1].NewBlock();
    }

    private void Update()
    {
        GetInput.Invoke();
        UpdateMiddle.Invoke();
    }

    
}
