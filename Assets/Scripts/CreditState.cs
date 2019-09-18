using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditState : IState
{
    public StateEvent OnStateSwitch { get; set; }

    private StateMachine stateMachine;

    private Button mainMenuButton = null;

    private GameObject canvas = null;
    private GameObject stateGroup = null;

    //Initialises or turns on the credit menu.
    public void OnEnter(MenuData _group)
    {
        if (stateGroup == null)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
            stateMachine = canvas.GetComponent<StateMachine>();

            stateGroup = Actor.Instantiate(_group.CreditGroup, canvas.GetComponent<Transform>().position, Quaternion.identity);
            stateGroup.transform.SetParent(canvas.transform);

            OnStateSwitch = stateMachine.SwitchState;

            mainMenuButton = stateGroup.transform.Find("MainMenuButton").GetComponent<Button>();
        }
        else
        {
            stateGroup.SetActive(true);
        }

        stateGroup.SetActive(true);

        mainMenuButton.onClick.AddListener(() => OnStateSwitch(typeof(MainMenuState)));
    }

    //Turns off the credits menu.
    public void OnExit()
    {
        stateGroup.SetActive(false);
    }
}