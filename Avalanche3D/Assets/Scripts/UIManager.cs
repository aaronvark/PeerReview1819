using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //Scripts
    public GameManager GameManagerInstance {get; private set;}

    //References
    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        GameManagerInstance = InstanceManager<GameManager>.GetInstance("GameManager");
        GameManagerInstance.onScoreChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        scoreText.text = "Max Height:" + GameManagerInstance.MaxHeight.ToString("F2");
    }
}
