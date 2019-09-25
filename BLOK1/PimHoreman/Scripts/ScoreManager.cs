using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ScoreManager class, updates the score and the highscore.
/// </summary>
public class ScoreManager : MonoBehaviour
{
	private const string KEY = "localHighscore";

	[SerializeField] private Text scoreText;
	[SerializeField] private Text highscoreText;
	[SerializeField] private string scoreString, highScoreString;

	private int scoreAmount;
	private int scoreTotal;

	private void Start()
    {
		UpdateLocalHighscore();
		scoreAmount = 0;
	}

	private void UpdateLocalHighscore()
	{
		highscoreText.text = highScoreString + PlayerPrefs.GetInt(KEY).ToString();
	}

	private void UpdateScore(int _score)
	{
		scoreAmount += _score;
		scoreTotal = scoreAmount;
		scoreText.text = scoreString + scoreAmount.ToString();
	}

	private void Update()
	{
		if(PlayerPrefs.GetInt(KEY) <= scoreAmount)
		{
			PlayerPrefs.SetInt(KEY, scoreTotal);
			UpdateLocalHighscore();
		}
	}

	private void OnEnable()
	{
		TargetPoints.TargetScoreEvent += UpdateScore;
		Bouncer.BouncerScoreEvent += UpdateScore;
		Brick.ScoreUpdateEvent += UpdateScore;
	}

	private void OnDisable()
	{
		TargetPoints.TargetScoreEvent -= UpdateScore;
		Bouncer.BouncerScoreEvent -= UpdateScore;
		Brick.ScoreUpdateEvent -= UpdateScore;
	}
}