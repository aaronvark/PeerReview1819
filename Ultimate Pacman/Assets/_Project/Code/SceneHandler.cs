using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void LoadNextScene() {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log(currentBuildIndex);
        Debug.Log(SceneManager.sceneCountInBuildSettings);

        if (currentBuildIndex < SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(currentBuildIndex + 1);
        }
        else {
            SceneManager.LoadScene(0);
        }
    }
}
