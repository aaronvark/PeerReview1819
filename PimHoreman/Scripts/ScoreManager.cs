using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	private int scoreAmount;
	private int scoreTotal;
	[SerializeField] private Text scoreText;

	// Start is called before the first frame update
	private void Start()
    {
		scoreAmount = 0;
	}

	private void UpdateScore(int _score)
	{
		scoreAmount += _score;
		scoreText.text = scoreAmount.ToString();
	}

	private void OnEnable()
	{
		Brick.ScoreUpdateEvent += UpdateScore;
	}

	private void OnDisable()
	{
		Brick.ScoreUpdateEvent -= UpdateScore;
	}
}
