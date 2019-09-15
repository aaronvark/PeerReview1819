using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{

    [SerializeField] private TextMeshPro player1ScoreText;
    [SerializeField] private TextMeshPro player2ScoreText;
    [SerializeField] private Transform player1EnergyBar;
    [SerializeField] private Transform player2EnergyBar;

    public void UpdatePlayerUI(int player1Score, int player1Energy, int player2Score, int player2Energy)
    {
        player1ScoreText.text = "" + player1Score;
        player1EnergyBar.GetChild(0).gameObject.SetActive(player1Energy > 0);
        player1EnergyBar.GetChild(1).gameObject.SetActive(player1Energy > 1);
        player1EnergyBar.GetChild(2).gameObject.SetActive(player1Energy > 2);

        player2ScoreText.text = "" + player2Score;
        player2EnergyBar.GetChild(0).gameObject.SetActive(player2Energy > 0);
        player2EnergyBar.GetChild(1).gameObject.SetActive(player2Energy > 1);
        player2EnergyBar.GetChild(2).gameObject.SetActive(player2Energy > 2);

    }
}