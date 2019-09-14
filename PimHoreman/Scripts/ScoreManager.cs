using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Text highscoreText;

	private int scoreAmount;
	private int scoreTotal;

	private const string KEY = "localHighscore";

	private void Start()
    {
		UpdateLocalHighscore();
		scoreAmount = 0;
	}

	private void UpdateLocalHighscore()
	{
		highscoreText.text = PlayerPrefs.GetInt(KEY).ToString();
	}

	private void UpdateScore(int _score)
	{
		scoreAmount += _score;
		scoreTotal = scoreAmount;
		scoreText.text = scoreAmount.ToString();
	}

	private void Update()
	{
		if(PlayerPrefs.GetInt(KEY) <= scoreAmount)
		{
			PlayerPrefs.SetInt(KEY, scoreTotal);
			UpdateLocalHighscore();
			Debug.Log(PlayerPrefs.GetInt(KEY).ToString());
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