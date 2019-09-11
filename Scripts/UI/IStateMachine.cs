using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SwitchState(States states);

public interface IState
{
    event SwitchState switchState;
    /// <summary>
    /// Enter the state
    /// </summary>
    void Enter();

    /// <summary>
    /// Exiting the state
    /// </summary>
    void Exit();

}
