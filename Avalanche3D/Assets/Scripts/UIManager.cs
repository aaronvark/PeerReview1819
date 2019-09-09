using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //Scripts
    GameManager gameManager;

    //References
    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onScoreChanged += UpdateUI;
    }

    void UpdateUI()
    {
        scoreText.text = "Max Height:" + gameManager.MaxHeight.ToString("F2");
    }
}
