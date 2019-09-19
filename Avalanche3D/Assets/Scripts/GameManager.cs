using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Delegates
    public delegate void OnScoreChanged();
    public OnScoreChanged onScoreChanged;

    public delegate void OnDeath();
    public OnDeath onDeath;

    public delegate void OnStartGame();
    public OnDeath onStartGame;

    //References
    public GameObject Player;

    //Public Variables
    public float MaxHeight;

    private void Awake()
    {
        onScoreChanged += ChangeScore;
        InstanceManager<GameManager>.CreateInstance("GameManager", this);

    }

    public void StartGame()
    {
        onStartGame();
    }

    public void ChangeScore()
    {
        float currentHeight = Player.transform.position.y;
        MaxHeight = currentHeight;
    }

    private void Update()
    {
        if(Player.transform.position.y > MaxHeight)
        {
            onScoreChanged();
        }
    }
}
