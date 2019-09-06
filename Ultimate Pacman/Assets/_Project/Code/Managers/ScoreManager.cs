using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public float CurrentScore { get; private set; } = 0f;

    public void AddScore(float value)
    {
        CurrentScore += value;
    }

    public void SubtractScore(float value)
    {
        CurrentScore -= value;
    }

    public void Reset()
    {
        CurrentScore = 0f;
    }
}
