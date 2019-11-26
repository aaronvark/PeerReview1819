using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class MenuTool : EditorWindow
{
    const string nameString = "Menu Maker";
    const string versionString = "V0.2.1";

    string titelString = "Titel Placeholder";

    string buttonStartString = "Start Name Placeholder";
    string buttonQuitString = "Quit Name Placeholder";
    string buttonString = "Button Name Placeholder";

    List<string> buttonStringList = new List<string>();

    [SerializeField]
    Object buttonSprite;
    [SerializeField]
    Object backgroundSprite;

    GameObject canvas;
    GameObject eventSystem;
    GameObject layoutGroupGameObject;

    string[] _choicesForButtons = new[] { "2", "3", "4", "5" };
    int buttonAmountInt = 0;

    //string[] _choicesForFontStyle = new[] { "Normal", "Bold", "Italic", "Bold And Italic" };
    //int fontStyleString = ;

    [MenuItem("Window/" + nameString + " " + versionString)]

    public static void ShowWindow()
    {
        GetWindow<MenuTool>($"{nameString} {versionString}");
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        titelString = EditorGUILayout.TextField("Titel", titelString);

        //fontStyleString = EditorGUILayout.Popup(fontStyleString, _choicesForFontStyle);

        backgroundSprite = EditorGUILayout.ObjectField(backgroundSprite, typeof(Sprite), true);

        GUILayout.Label("Button Settings", EditorStyles.boldLabel);
        buttonAmountInt = EditorGUILayout.Popup(buttonAmountInt, _choicesForButtons);

        buttonStartString = EditorGUILayout.TextField("Start button", buttonStartString);
        buttonQuitString = EditorGUILayout.TextField("Quit button", buttonQuitString);

        int i = 0;

        while (i < buttonAmountInt)
        {
            //Todo : Fix entering the text for the button via a list or array
            if (buttonStringList.ElementAtOrDefault(i) == null)
            {
                buttonStringList.Add(EditorGUILayout.TextField("button" + i, buttonString));
            }
            else
            {
                buttonStringList[i] = EditorGUILayout.TextField("button" + i, buttonStringList[i]);
            }
            i++;
        }

        buttonSprite = EditorGUILayout.ObjectField(buttonSprite, typeof(Sprite), true);

        if (GUILayout.Button("Generate"))
        {
            GenerateMenu();
            buttonStringList.ForEach(Debug.Log);
        }
    }

    //This is where the magic happens.------------------------------------------------------------------------------------------

    void GenerateMenu()
    {
        // Todo : Look into why this works.
        // Also his is stupid
        canvas = new GameObject();
        layoutGroupGameObject = new GameObject();

        //Canvas and Event system.
        CustomCore Core = new CustomCore(canvas, eventSystem);

        //Background and titel
        CustomStatic Static = new CustomStatic(canvas, backgroundSprite, titelString, layoutGroupGameObject);

        //Buttons
        CustomButton StartCustomButton = new CustomButton("StartButton", buttonStartString, buttonSprite, layoutGroupGameObject);

        foreach (string str in buttonStringList)
        {
            CustomButton CustomButton = new CustomButton(str, str, buttonSprite, layoutGroupGameObject);
        }

        CustomButton QuitCustomButton = new CustomButton("QuitButton", buttonQuitString, buttonSprite, layoutGroupGameObject);
    }
}

public class CustomCore
{
    public CustomCore(GameObject canvas, GameObject eventSystem)
    {
        //Setup of canvas
        canvas.name = "Canvas";
        canvas.AddComponent<RectTransform>();
        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        //Setup of event system
        eventSystem = new GameObject();
        eventSystem.name = "EventSystem";
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }
}

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
        titel.transform.localPosition = new Vector3(0, Screen.height / 4, 0); //Todo : set localposition properly.
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
        layoutGroupGameObject.GetComponent<VerticalLayoutGroup>().padding.top = Screen.height - Screen.height / 4; //Todo : Look into making this better.
        layoutGroupGameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = Screen.height / 16;
    }
}

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