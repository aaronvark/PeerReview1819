using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : IState
{
    public event SwitchState switchState;

    private List<Button> buttons = new List<Button>();
    private GameObject panel;

    public MainMenuState(Button _playButton, Button _optionsButton, Button _exitButton,GameObject _panel)
    {
        buttons.Add(_playButton);
        buttons.Add(_optionsButton);
        buttons.Add(_exitButton);

        panel = _panel;
    }
    public void Enter()
    {
        panel.gameObject.SetActive(true);   
        buttons[(int)MainMenuButtons.PlayButton].onClick.AddListener(() => switchState(States.PlayState));
        buttons[(int)MainMenuButtons.OptionsButton].onClick.AddListener(() => switchState(States.OptionState));
        buttons[(int)MainMenuButtons.ExitButton].onClick.AddListener(() => switchState(States.ExitState));

    }

    public void Exit()
    {
        for (int i = 0; i < buttons.Count; i++)
            buttons[i].onClick.RemoveAllListeners();

        buttons = new List<Button>();

        panel.gameObject.SetActive(false);
    }
}

public enum MainMenuButtons
{
    PlayButton,
    OptionsButton,
    ExitButton
}