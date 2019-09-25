using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private void Start()
    {
        _instance = this;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSpecificScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void Reload()
    {
        Debug.Log("Reload");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        Debug.Log("You Win");
        LoadNextScene();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("I have Quit the game");
    }

}
