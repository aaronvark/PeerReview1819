using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start() {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        scoreText.text = ScoreManager.Instance?.getPoints().ToString() + " : " + ScoreManager.Instance?.GetHighScore().ToString();
    }
}
