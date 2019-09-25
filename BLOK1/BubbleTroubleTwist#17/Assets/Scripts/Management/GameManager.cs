using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnGameUpdate();

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.OnGameOverHandler += GameOver;
    }

    public void GameOver()
    {
        StartCoroutine(LoadSceneAsyncInBackground());
    }

    public System.Collections.IEnumerator LoadSceneAsyncInBackground()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
