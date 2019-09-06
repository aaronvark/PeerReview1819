using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    public void LoadNextScene()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentBuildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentBuildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
