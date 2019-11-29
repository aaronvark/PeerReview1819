using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton
{
    public CustomButton(string buttonName, string buttonText, Object buttonSprite, GameObject layoutGroupGameObject)
    {
        //Setup for button
        GameObject button = new GameObject();
        button.name = buttonName;
        button.transform.parent = layoutGroupGameObject.transform;
        button.AddComponent<RectTransform>().sizeDelta = new Vector2(150, 50);
        button.AddComponent<CanvasRenderer>();
        button.AddComponent<Image>().sprite = buttonSprite as Sprite;
        button.GetComponent<Image>().type = Image.Type.Sliced;
        button.AddComponent<Button>();
            
        //Create and add the background image to the button
        GameObject buttonTextObject = new GameObject();
        buttonTextObject.name = buttonName + "Text";
        buttonTextObject.transform.parent = button.transform;
        buttonTextObject.transform.localPosition = new Vector3(0, 0, 0);
        buttonTextObject.AddComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        buttonTextObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        buttonTextObject.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, 0);
        buttonTextObject.AddComponent<Text>().text = buttonText;
        buttonTextObject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        buttonTextObject.GetComponent<Text>().resizeTextForBestFit = true;
        buttonTextObject.GetComponent<Text>().color = new Color(0, 0, 0);
    }
}
