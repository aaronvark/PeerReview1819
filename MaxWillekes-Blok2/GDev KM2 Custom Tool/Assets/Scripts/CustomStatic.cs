using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomStatic
{
    public CustomStatic(GameObject canvas, Object backgroundSprite, string titelString, GameObject layoutGroupGameObject)
    {
        //Setup of the background
        GameObject background = new GameObject();
        background.transform.parent = canvas.transform;
        background.transform.localPosition = new Vector3(0, 0, 0);
        background.name = "Background";
        background.AddComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        background.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        background.AddComponent<Image>().sprite = backgroundSprite as Sprite;

        //Setup of Titel
        GameObject titel = new GameObject();
        titel.transform.parent = canvas.transform;
        titel.transform.localPosition = new Vector3(0, Screen.height / 4, 0);
        titel.name = "Titel";
        titel.AddComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        titel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        titel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        titel.AddComponent<Text>().text = titelString;
        titel.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        titel.GetComponent<Text>().fontSize = 28;
        titel.GetComponent<Text>().fontStyle = FontStyle.Normal;
        titel.GetComponent<Text>().color = new Color(0, 0, 0);

        //Setup for the layout of the buttons
        layoutGroupGameObject.name = "ButtonLayoutGroup";
        layoutGroupGameObject.transform.parent = canvas.transform;
        layoutGroupGameObject.transform.localPosition = new Vector2(0, 0);
        layoutGroupGameObject.AddComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        layoutGroupGameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        layoutGroupGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        layoutGroupGameObject.AddComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        layoutGroupGameObject.GetComponent<VerticalLayoutGroup>().padding.top = Screen.height / 4;
        layoutGroupGameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = Screen.height / 16;
    }
}
