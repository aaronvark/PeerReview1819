using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public float blockSpeed = 1;
    public float middleForce = 1;
    public float middleRotateSpeed = 1;

    private List<Block> blocks = new List<Block>();
    //is this usefull or unneccesary?
    public List<Block> GetBlocks() { return blocks; }
    public void AddBlock(Block _block) { blocks.Add(_block);}
    public void RemoveBlock(Block _block) { blocks.Remove(_block); }

    public GameObject blockPrefab;

    //TODO: other way without player references?
    public Player[] players = new Player[2];

    public event System.Action GetInput;
    public event System.Action UpdateMiddle;

    private PlayerInput playerInput;
    private Middle middle;
    public BlockPool blockPool;

    private void Awake()
    {
        if ( instance != null && instance != this){ Destroy(this.gameObject); }
        else{ instance = this; }
    }

    private void Start()
    {
        playerInput = new PlayerInput();
        blockPool = new BlockPool(blockPrefab,20);
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
