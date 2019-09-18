using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    private int score;

    private Text scoreText;
    
    //Adds a random number between 1 and 5 to score.
    //Updates scoreText with the new score number for display to the player.
    public void ScoreUp()
    {
        score += Random.Range(1, 5);

        scoreText.text = "Score : " + score;
    }

    //Sets scoreText as the Text component with the tag "ScoreText" in the scene.
    //Sets score to 0.
    private void Awake()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        score = 0;
    }
}
