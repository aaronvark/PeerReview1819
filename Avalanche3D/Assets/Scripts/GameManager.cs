using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Delegates
    public delegate void OnScoreChanged();
    public OnScoreChanged onScoreChanged;

    //Singleton
    public static GameManager Instance;

    //References
    public GameObject Player;

    //Public Variables
    public float MaxHeight;

    private void Awake()
    {
        Instance = this;
        onScoreChanged += ChangeScore;
        InstanceManager<GameManager>.CreateInstance("GameManager", this);

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
