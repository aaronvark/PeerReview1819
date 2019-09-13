using UnityEngine;
using UnityEngine.UI;

namespace Breakin
{
    [RequireComponent(typeof(Text))]
    public class ScoreViewer : MonoBehaviour
    {
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            // When the score in the score manager is being updated, the displayed score should be updated
            ScoreManager.Instance.ScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            ScoreManager.Instance.ScoreChanged -= UpdateScore;
        }

        private void UpdateScore()
        {
            text.text = "Score: " + ScoreManager.Instance.Score;
        }
    }
}