using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Color allTextColor = new Color();
    public Color AllTextColor
    {
        get
        {
            return allTextColor;
        }
    }

    [SerializeField] private Color[] levelSelectButtonColors = new Color[2];
    public Color[] LevelSelectButtonColors
    {
        get { return levelSelectButtonColors; }
        set
        {
            levelSelectButtonColors = value;
        }
    }

    [SerializeField] private List<Button> levelSelectButtons = new List<Button>();
    public List<Button> LevelSelectButtons
    {
        get
        {
            return levelSelectButtons;
        }
    }

    private List<TextMeshProUGUI> allUIText = new List<TextMeshProUGUI>();

    private List<Level> levels = new List<Level>();

    private void Start()
    {
        allUIText = FindObjectsOfType<TextMeshProUGUI>().ToList();
        if (allUIText == null) return;
        foreach (var text in allUIText)
        {
            text.color = AllTextColor;
        }
        if (LevelSelectButtons.Count < 1) return;
        levels = LevelManager.Instance.GiveLevels();
        LevelSelectButtons[0].image.color = Color.green;
        for (int i = 1; i < LevelSelectButtons.Count; i++)
        {
            LevelSelectButtons[i].enabled = levels[i - 1].Done;
            if (LevelSelectButtons[i].enabled)
            {
                LevelSelectButtons[i].image.color = LevelSelectButtonColors[0];
            }
            else
            {
                LevelSelectButtons[i].image.color = LevelSelectButtonColors[1];
            }
        }

    }

    public void SelectLevel(int levelIndex)
    {
        EventManagerGen<int>.BroadCast(EVENT.selectGame, levelIndex);
    }



    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

}
