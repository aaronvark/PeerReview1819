using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
