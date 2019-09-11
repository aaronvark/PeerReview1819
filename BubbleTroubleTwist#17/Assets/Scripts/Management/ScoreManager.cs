using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public string scoreTextString;
    public int Score { get; set; }

    private void Start()
    {
        UpdateScoreText();
        EventManager.OnScoreChangedHandler += UpdateScore;
    }

    public void UpdateScore(int _amount)
    {
        Score += _amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = scoreTextString + Score;
    }

    private void OnDisable()
    {
        EventManager.OnScoreChangedHandler -= UpdateScore;
    }
}
