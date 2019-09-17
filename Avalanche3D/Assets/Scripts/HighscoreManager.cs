using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static float ScoresAmount = 10;
    public static List<float> Highscores = new List<float>();

    public static void AddHighScore(float Score)
    {
        if (Highscores.Count < ScoresAmount)
        {
            Highscores.Add(Score);
        }
    }
}
