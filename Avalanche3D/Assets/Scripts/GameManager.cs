using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

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
