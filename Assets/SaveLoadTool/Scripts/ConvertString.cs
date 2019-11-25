using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertString
{
    private const string Vector3Name = "Vector3";
    private const string QuaternionName = "Quaternion";

    public static object ThisType<T>(string value, T toType) where T : System.Type
    {
        switch (toType.Name)
        {
            case Vector3Name:
                return ToVector3(value);
            case QuaternionName:
                return ToQuaternion(value);
            default:
                break;
        }

        Debug.LogError("This type is not yet supported!");
        return null;
    }

    public static Vector3 ToVector3(string text)
    {
        string[] splitText = TrimSplit(text);
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

    private static Quaternion ToQuaternion(string text)
    {
        string[] splitText = TrimSplit(text);
        Quaternion quaternion = new Quaternion();

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
                    quaternion.x = nFloat;
                    break;
                case 1:
                    quaternion.y = nFloat;
                    break;
                case 2:
                    quaternion.z = nFloat;
                    break;
                case 3:
                    quaternion.w = nFloat;
                    break;
                default:
                    break;
            }
        }

        return quaternion;
    }

    private static string[] TrimSplit(string text)
    {
        return text.Trim('(', ')').Split(',');
    }
}