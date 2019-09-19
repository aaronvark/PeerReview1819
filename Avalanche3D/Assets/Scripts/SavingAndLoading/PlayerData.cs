using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public List<HighscoreEntry> Highscores;

    public PlayerData(List<HighscoreEntry> highscores)
    {
        Highscores = highscores;
    }
}
