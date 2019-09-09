using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public float CurrentScore { get; private set; } = 0f;

    public void AddScore(IScore score)
    {
        CurrentScore += score.ScoreValue;
    }

    public void SubtractScore(IScore score)
    {
        CurrentScore -= score.ScoreValue;
    }

    public void Reset()
    {
        CurrentScore = 0f;
    }
}
