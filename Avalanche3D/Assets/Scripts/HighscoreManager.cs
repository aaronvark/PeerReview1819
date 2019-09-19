using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class HighscoreManager : MonoBehaviour
{
    GameManager GameManagerInstance;

    public static float ScoresAmount = 10;
    public static List<HighscoreEntry> Highscores = new List<HighscoreEntry>();

    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private Transform HighscorePanel;
    [SerializeField]
    private GameObject Prefab;
    [SerializeField]
    private string CurrentName;

    private void Start()
    {
        GameManagerInstance = InstanceManager<GameManager>.GetInstance("GameManager");
        GameManagerInstance.onDeath += AddHighScore;
        GameManagerInstance.onDeath += ShowScores;
        GameManagerInstance.onStartGame += EnterName;

        gameObject.SetActive(false);
    }

    private void AddHighScore()
    {
        float Score = GameManagerInstance.MaxHeight;
        if (Highscores.Count < ScoresAmount)
        {
            HighscoreEntry entry = new HighscoreEntry(CurrentName, Score);

            Highscores.Add(entry);
            int index = Highscores.IndexOf(entry);
            Highscores = Highscores.OrderByDescending(x => x.Score).ToList();
            if (Highscores.Count > 10)
            {
                Highscores.Remove(Highscores[10]);
            }
        }
        SaveSystem.SavePlayer(Highscores);

    }

    private void ShowScores()
    {
        Highscores = Highscores.Union<HighscoreEntry>(SaveSystem.LoadPlayer().Highscores).ToList<HighscoreEntry>();

        for (int i = 0; i < Highscores.Count; i++)
        {
            HighscorePanel.gameObject.SetActive(true);
            GameObject ScorePane = Instantiate(Prefab, HighscorePanel);
            ScorePane.GetComponent<ScorePane>().SetText(i + 1, Highscores[i].Name, Highscores[i].Score);
        }
    }

    public void EnterName()
    {
        inputField.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        CurrentName = inputField.text;
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public string Name;
    public float Score;

    public HighscoreEntry(string name, float score)
    {
        Name = name;
        Score = score;
    }
}

