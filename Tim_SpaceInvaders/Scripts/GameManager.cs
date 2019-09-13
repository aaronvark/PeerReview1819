using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ObjectPoolManager))]
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public int highScore;
    public int score;

    private Player playerObject;

    private void Start()
    {
        playerObject = Player.Instance;
    }

    private void StartGame() {

    }

    public void GameOver()
    {
        Debug.Log("Game over.");

        //TODO game over uitwerken.
    }

}
