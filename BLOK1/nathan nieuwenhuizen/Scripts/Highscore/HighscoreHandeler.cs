using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the score data by using playerprefs
/// </summary>
public class HighscoreHandeler : MonoBehaviour
{
    public static string HIGHSCORE_KEY = "highscore";
    public static int HighScore
    {
        get {
            return PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        }
        set
        {
            if (value > HighScore)
            {
                PlayerPrefs.SetInt(HIGHSCORE_KEY, value);
            }
        }
    }
}
