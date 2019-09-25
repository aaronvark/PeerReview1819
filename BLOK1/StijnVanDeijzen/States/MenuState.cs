using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuState : IGameState
{
    public SimpleStateEvent OnStateSwitch { get; set; }
    public GameManager gameManager { get; set; }

    public void End()
    {

    }

    public void Run()
    {
        if (StartButton.isClicked)
        {
            OnStateSwitch(new PlayState(), "MainGame");
        }
    }

    public void Start()
    {
    }
}