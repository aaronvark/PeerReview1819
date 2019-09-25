using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject ingameUI;
    public GameObject gameOverMenu;


    public static UIManager Instance { get; private set; }


    public void EnableMenu(GameObject _obj)
    {
        _obj.SetActive(!_obj.activeSelf);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        EnableMenu(ingameUI);
        EnableMenu(gameOverMenu);
    }
}
