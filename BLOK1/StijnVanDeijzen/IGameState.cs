using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    GameManager gameManager { get; set; }
    SimpleStateEvent OnStateSwitch { get; set; }
    void Start();
    void Run();
    void End();
}

