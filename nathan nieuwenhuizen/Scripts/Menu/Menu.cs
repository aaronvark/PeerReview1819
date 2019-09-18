using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Text highscoreText;

    void Start()
    {
        highscoreText.text ="Highscore:  " + HighscoreHandeler.HighScore;
    }
}
