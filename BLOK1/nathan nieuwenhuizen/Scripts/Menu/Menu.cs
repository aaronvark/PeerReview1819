using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This only shows the highscore at the menu screen ;p
/// </summary>
public class Menu : MonoBehaviour
{
    [SerializeField]
    private Text highscoreText;

    void Start()
    {
        highscoreText.text ="Highscore:  " + HighscoreHandeler.HighScore;
    }
}
