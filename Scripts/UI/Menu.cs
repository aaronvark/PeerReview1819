using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //OptionMenu
    [Header("OptionsMenu")]
    [SerializeField]
    private GameObject optionMenu;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle fullScreenToggle;
    [SerializeField]
    private Button applyButton;

    [Space(10)]

    //MainMenu
    [Header("Main Menu")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button exitButton;

    private StateMachine stateMachine;
    private List<IState> states = new List<IState>();

    private void Awake()
    {
        states.Add(new MainMenuState(playButton, optionsButton, exitButton,mainMenu));
        states.Add(new PlayState());
        states.Add(new OptionState(optionMenu, volumeSlider, fullScreenToggle, applyButton));
        states.Add(new ExitState());

        stateMachine = new StateMachine(states);

        for (int i = 0; i < states.Count; i++)
            states[i].switchState += stateMachine.SwitchState;

        Debug.Log(GetInstanceID());
        stateMachine.SwitchState(States.MainMenu);
        
    }
}
