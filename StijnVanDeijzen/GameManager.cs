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

    //DISCUSS: other way without player references?
    public Player[] players = new Player[2];

    private PlayerInput playerInput;
    public UI UI;

    public delegate void AttractDelegate(Vector3 target, float force);
    public AttractDelegate Attract;

    private void Awake()
    {
        if ( instance != null && instance != this){ Destroy(this.gameObject); }
        else{ instance = this; }
    }

    private void Start()
    {
        playerInput = new PlayerInput();


        //DISCUSS: Replace w event?
        players[0].NewBlock();
        players[1].NewBlock();                
    }

    private void OnDestroy()
    {
        Attract = null;
    }

    private void Update()
    {
        //DISCUSS: Replace w event?
        players[0].ProcessInput(playerInput.GetInput(0));
        players[1].ProcessInput(playerInput.GetInput(1));

        if (Attract != null) { Attract(transform.position, middleForce); }

        UI.UpdatePlayerUI(players[0].score, players[0].energy, players[1].score, players[1].energy);
    }

    
}
