using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Color allTextColor;
    protected List<TextMeshProUGUI> allUIText { get; set; }


    private void Start()
    {
        allUIText = FindObjectsOfType<TextMeshProUGUI>().ToList();
        foreach(var text in allUIText)
        {
            text.color = allTextColor;
        }
    }

}
