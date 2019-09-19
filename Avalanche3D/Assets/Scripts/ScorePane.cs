using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScorePane : MonoBehaviour
{
    public Text NumberText;
    public Text NameText;
    public Text ScoreText;

    public void SetText(int number, string name, float score)
    {
        NumberText.text = number + ".";
        NameText.text = name;
        ScoreText.text = score.ToString("F2");
    }

}
