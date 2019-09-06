using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private float scorePoints;
    public float highScore;



    // adds points to the current score value
    public void addPoint(float _points) {
        scorePoints += _points;
    }
    // Gets points and returns a float value
    public float getPoints() {
        return scorePoints;
    }

    public void SetHighScore()
    {
        if (!PlayerPrefs.HasKey("highScore"))
        {
            PlayerPrefs.SetFloat("highScore", 0);
        }
        float _tempHighScore = PlayerPrefs.GetFloat("highScore");
        if (highScore > _tempHighScore)
        {
            PlayerPrefs.SetFloat("highScore", highScore);
        }
    }

    // creates  a singleton instance of the object
    private void Awake() { 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

       // DontDestroyOnLoad(gameObject);
    }
}
