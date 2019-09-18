using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{

    [SerializeField] private TextMeshPro player1ScoreText;
    [SerializeField] private TextMeshPro player2ScoreText;

    public void UpdateUI(Player[] players)
    {
        player1ScoreText.text = "" + players[0].score;
        player2ScoreText.text = "" + players[1].score;

    }
}