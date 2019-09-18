using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuState : IState
{
    public StateEvent OnStateSwitch { get; set; }

    private StateMachine stateMachine;

    private Button creditsButton = null;
    private Button startButton = null;
    private Button exitButton = null;

    private GameObject canvas = null;
    private GameObject stateGroup = null;

    //Initialises or if it already excists turns on the main menu.
    public void OnEnter(MenuData group)
    {
        if (stateGroup != null)
        {
            stateGroup.SetActive(true);
        }
        else
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
            stateMachine = canvas.GetComponent<StateMachine>();

            stateGroup = Actor.Instantiate(group.MenuGroup, canvas.GetComponent<Transform>().position, Quaternion.identity);
            stateGroup.transform.SetParent(canvas.transform);

            OnStateSwitch = stateMachine.SwitchState;

            creditsButton = stateGroup.transform.Find("CreditsButton").GetComponent<Button>();
            startButton = stateGroup.transform.Find("StartButton").GetComponent<Button>();
            exitButton = stateGroup.transform.Find("ExitButton").GetComponent<Button>();
        }

        stateGroup.SetActive(true);

        creditsButton.onClick.AddListener(() => OnStateSwitch(typeof(CreditState)));
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene("SampleScene");
            Cursor.visible = false;
        });
        exitButton.onClick.AddListener(() => Application.Quit());
    }

    //Turns of the main menu.
    public void OnExit()
    {
        stateGroup.SetActive(false);
    }
}