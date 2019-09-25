using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIManager class, manages the UI, for now the total of ball in the playing field left (ScoreManager could be implemented in the UIManager).
/// </summary>
public class UIManager : MonoBehaviour
{
	[SerializeField] private Text ballAmountText;
	[SerializeField] private Ball ball;
	[SerializeField] private string ballString;

	private void Start()
	{
		ballAmountText.text = ballString + ball.Health.ToString();
	}

	private void Update()
	{
		DisplayBallText();
	}

	private void DisplayBallText()
	{
		ballAmountText.text = ballString + ball.Health.ToString();
	}
}