using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertStringTo
{
    public static Vector3 Vector3(string text)
    {
        text = text.Trim('(', ')');
        string[] splitText = text.Split(',');
        Vector3 vector = new Vector3();

        for (int i = 0; i < splitText.Length; i++)
        {
            string[] splitValue = splitText[i].Split('.');
            float nFloat;
            if (splitValue.Length < 2)
            {
                nFloat = float.Parse(splitText[i]);
            } else
            {
                nFloat = float.Parse(splitText[i]) / Mathf.Pow(10, splitValue[1].Length);
            }

            switch (i)
            {
                case 0:
                    vector.x = nFloat;
                    break;
                case 1:
                    vector.y = nFloat;
                    break;
                case 2:
                    vector.z = nFloat;
                    break;
                default:
                    break;
            }
        }

        return vector;
    }

    public static string XMLFormat(string text)
    {
        return text.Replace(' ', '_').Replace('-', '_');
    }
}
