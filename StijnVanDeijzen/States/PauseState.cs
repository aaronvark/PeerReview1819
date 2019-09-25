using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IGameState
{
    public SimpleStateEvent OnStateSwitch { get; set; }
    public GameManager gameManager { get; set; }

    public void End()
    {
        Time.timeScale = 1;
    }

    public void Run()
    {
        if (PlayerInput.GetPause())
        {
            OnStateSwitch(new PlayState());
        }
    }

    public void Start()
    {
        Time.timeScale = 0;
    }
}