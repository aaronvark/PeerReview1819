using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { 

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public float blockSpeed = 1;
    public float middleForce = 1;
    public float middleRotateSpeed = 1;

    public List<Block> blocks;
    public GameObject[] blockPrefabs;
    public Player[] players = new Player[2];
    

    private void Awake()
    {
        if ( _instance != null && _instance != this){ Destroy(this.gameObject); }
        else{ _instance = this; }
    }    

    void Start()
    {
        players[0].NewBlock();
        players[1].NewBlock();
    }

    
    void Update()
    {
        //Player1
        GameManager.Instance.players[0].Move(Input.GetKey(KeyCode.UpArrow)); 
        if (Input.GetKey(KeyCode.LeftArrow)) { GameManager.Instance.players[0].Rotate(-1); }
        else if (Input.GetKey(KeyCode.RightArrow)) { GameManager.Instance.players[0].Rotate(1); }
        //Player2
        GameManager.Instance.players[1].Move(Input.GetKey(KeyCode.W));
        if (Input.GetKey(KeyCode.A)) { GameManager.Instance.players[1].Rotate(-1); }
        else if (Input.GetKey(KeyCode.D)) { GameManager.Instance.players[1].Rotate(1); }
    }
}
