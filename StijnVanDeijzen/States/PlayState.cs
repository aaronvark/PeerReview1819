using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayState : IGameState
{
    public SimpleStateEvent OnStateSwitch { get; set; }
    public GameManager gameManager { get; set; }


    public void End()
    {
    }

    public void Run()
    {
        if (PlayerInput.GetPause())
        {
            OnStateSwitch(new PauseState());
        }

        gameManager.players[0].ProcessInput(1);
        gameManager.players[1].ProcessInput(2);
        if (gameManager.Attract != null) { gameManager.Attract(new Vector2(0, 0), GameManager.middleForce); }

        gameManager.UI.UpdateUI(gameManager.players);


    }

    public void Start()
    {
        gameManager.InitializeGame();
    }
}
