using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    //References
    public GameObject Player;

    //Public Variables
    public float Score;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(float score)
    {
        score += Score;
    }
}
