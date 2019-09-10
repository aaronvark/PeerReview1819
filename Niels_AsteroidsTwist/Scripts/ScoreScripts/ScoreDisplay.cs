using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Start() {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        scoreText.text = ScoreManager.Instance.getPoints().ToString();
    }
}
