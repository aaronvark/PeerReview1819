using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateEvent(System.Type state);

public interface IState
{
    StateEvent OnStateSwitch { get; set; }
    void OnEnter(MenuData menuData);
    void OnExit();
}