using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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