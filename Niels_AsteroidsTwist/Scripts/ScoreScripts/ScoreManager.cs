using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private float scorePoints;

    // adds points to the current score value
    public void addPoint() {
        scorePoints += 1f;
    }
    // Gets points and returns a float value
    public float getPoints() {
        return scorePoints;
    }

    // creates  a singleton instance of the object
    private void Awake() { 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
