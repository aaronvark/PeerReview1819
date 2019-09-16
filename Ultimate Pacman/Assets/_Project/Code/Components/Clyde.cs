using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : Ghost
{
    private void Start()
    {
        defaultState = new FakeOutState(transform);
        stateMachine = new StateMachine(defaultState);
    }
}
