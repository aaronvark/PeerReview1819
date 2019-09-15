using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Color allTextColor;
    protected List<TextMeshProUGUI> allUIText { get; set; }


    private void Start()
    {
        allUIText = FindObjectsOfType<TextMeshProUGUI>().ToList();
        if (allUIText == null) return;
        foreach(var text in allUIText)
        {
            text.color = allTextColor;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

}
