using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype1
{
    public class ScoreViewer : MonoBehaviour
    {
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScore;
        }

        private void UpdateScore()
        {
            text.text = "Score: " + ScoreManager.Instance.Score;
        }
    }
}