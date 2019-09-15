using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public float blockSpeed = 1;
    public float middleForce = 1;

    //TODO: set rotate speed in animator
    //public float middleRotateSpeed = 1;  

    private List<Block> blocks = new List<Block>();
    //is this usefull or unneccesary?
    public List<Block> GetBlocks() { return blocks; }
    public void AddBlock(Block _block) { blocks.Add(_block);}
    public void RemoveBlock(Block _block) { blocks.Remove(_block); }

    public GameObject blockPrefab;

    //TODO: other way without player references?
    public Player[] players = new Player[2];

    public event System.Action UpdateUI;

    private PlayerInput playerInput;
    public BlockPool blockPool;
    public UI UI;

    public delegate void AttractDelegate(Vector3 target, float force);
    public AttractDelegate Attract;
    public Transform middlepoint;

    private void Awake()
    {
        if ( instance != null && instance != this){ Destroy(this.gameObject); }
        else{ instance = this; }
    }

    private void Start()
    {
        playerInput = new PlayerInput();
        blockPool = new BlockPool(blockPrefab,20);

        players[0].NewBlock();
        players[1].NewBlock();

        
    }

    private void OnDestroy()
    {
        Attract = null;
    }

    private void Update()
    {        
        players[0].ProcessInput(playerInput.GetInput(0));
        players[1].ProcessInput(playerInput.GetInput(1));

        if (Attract != null) { Attract(transform.position, middleForce); }

        UI.UpdatePlayerUI(players[0].score, players[0].energy, players[1].score, players[1].energy);
    }

    
}
