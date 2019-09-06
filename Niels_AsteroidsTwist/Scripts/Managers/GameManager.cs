using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneLoader SceneManager;

    public static System.Action endGameSystems;
    // Start is called before the first frame update

    public static void OnReset()
    {
        endGameSystems();
    }

    private void Start()
    {
        endGameSystems += ScoreManager.Instance.SetHighScore;
        endGameSystems += SceneManager.ResetScene;
    }

    private void OnDisable()
    {
        endGameSystems -= ScoreManager.Instance.SetHighScore;
        endGameSystems -= SceneManager.ResetScene;
    }

    // Update is called once per frame




}
