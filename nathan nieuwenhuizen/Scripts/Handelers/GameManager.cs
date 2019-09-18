using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private WaveManager asteroidSpawner;
    [SerializeField]
    private Character character;

    [SerializeField]
    private GameObject endScreen;
    [SerializeField]
    private Text endMessage;

    [SerializeField]
    private GameObject pauseScreen;

    private static GameManager instance;
    public static GameManager Instance
    {
        get {
            return instance;
        }
        set {
            Instance = value;
        }
    }

    void Start()
    {
        instance = this;

        asteroidSpawner.Character = character;
        asteroidSpawner.StartWave();
    }

    public void Pause(bool _pause)
    {
        Time.timeScale = _pause ? 0 : 1f;
        pauseScreen.SetActive(_pause);
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void EndScreen()
    {
        endScreen.SetActive(true);
        endMessage.text = "You survived to wave   " + WaveManager.Instance.Wave;
        if (WaveManager.Instance.Wave > HighscoreHandeler.HighScore)
        {
            HighscoreHandeler.HighScore = WaveManager.Instance.Wave;
            endMessage.text += "\n NEW HIGHSCORE!";
        } 
    }
}
