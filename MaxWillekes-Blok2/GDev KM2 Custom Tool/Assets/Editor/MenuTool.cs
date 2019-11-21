using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuTool : EditorWindow
{
    const string nameString = "Menu Maker";
    const string versionString = "V0.0.31";

    string titelString = "Titel Placeholder";

    string buttonStartString = "Start Name Placeholder";
    string buttonQuitString = "Quit Name Placeholder";
    string button1String = "Button Name Placeholder";
    string button2String = "Button Name Placeholder";
    string button3String = "Button Name Placeholder";

    public Object buttonSprite;
    public Object backgroundSprite;

    GameObject canvas;
    GameObject eventSystem;
    GameObject layoutGroupGameObject;

    string[] _choicesForButtons = new[] { "2", "3", "4", "5" };
    int buttonAmountInt = 0;

    //string[] _choicesForFontStyle = new[] { "Normal", "Bold", "Italic", "Bold And Italic" };
    //int fontStyleString = ;

    [MenuItem ("Window/" + nameString + " " + versionString)]

    public static void ShowWindow()
    {
        GetWindow<MenuTool>($"{nameString} {versionString}");
    }

    void OnGUI()
    {
        GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
        titelString = EditorGUILayout.TextField ("Titel", titelString);

        //fontStyleString = EditorGUILayout.Popup(fontStyleString, _choicesForFontStyle);

        backgroundSprite = EditorGUILayout.ObjectField(backgroundSprite, typeof(Sprite), true);

        GUILayout.Label("Button Settings", EditorStyles.boldLabel);
        buttonAmountInt = EditorGUILayout.Popup(buttonAmountInt, _choicesForButtons);
        Debug.Log(buttonAmountInt);

        buttonStartString = EditorGUILayout.TextField("Start button", buttonStartString);
        buttonQuitString = EditorGUILayout.TextField("Quit button", buttonQuitString);

        int i = 1;

        while (i <= buttonAmountInt)
        {
            //Todo : Fix entering the text for the button via a list or array
            button1String = EditorGUILayout.TextField("button" + i, button1String);
            i++;
        }

        buttonSprite = EditorGUILayout.ObjectField(buttonSprite, typeof(Sprite), true);

        if (GUILayout.Button("Generate"))
        {
            GenerateMenu(buttonAmountInt);
        }
    }

    //This is where the magic happens.------------------------------------------------------------------------------------------

    void GenerateMenu(int buttonAmountToGenerateInt)
    {
        //Canvas and Event system.
        GenerateCore();

        //Background and titel
        GenerateStatic();

        //Buttons

        //Add 2 for the quit and start buttons
        buttonAmountToGenerateInt += 2;
 
        int j = 1;
        while (j <= buttonAmountToGenerateInt)
        {
            string buttonNameToGiveString;
            string buttonTextToGiveString;

            if ( j == 1)
            {
                buttonNameToGiveString = "StartButton";
                buttonTextToGiveString = buttonStartString;
            }
            else if ( j == buttonAmountToGenerateInt)
            {
                buttonNameToGiveString = "QuitButton";
                buttonTextToGiveString = buttonQuitString;
            }
            else {
                buttonNameToGiveString = "buttonNameToGiveString" + j;
                buttonTextToGiveString = "buttonTextToGiveString" + j;
            }

            GenerateButtons(buttonNameToGiveString, buttonTextToGiveString);
            j++;
        }
    }

    void GenerateCore()
    {
        //Setup of canvas
        canvas = new GameObject();
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

    void GenerateStatic()
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
        titel.transform.localPosition = new Vector3(0, Screen.height/4, 0); //Todo : set localposition properly.
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
        layoutGroupGameObject = new GameObject();
        layoutGroupGameObject.name = "ButtonLayoutGroup";
        layoutGroupGameObject.transform.parent = canvas.transform;
        layoutGroupGameObject.transform.localPosition = new Vector2(0, 0);
        layoutGroupGameObject.AddComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        layoutGroupGameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        layoutGroupGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        layoutGroupGameObject.AddComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
        layoutGroupGameObject.GetComponent<VerticalLayoutGroup>().padding.top = Screen.height / 2;
        layoutGroupGameObject.GetComponent<VerticalLayoutGroup>().padding.bottom = Screen.height / 16;
    }

    void GenerateButtons(string buttonName, string buttonText)
    {
        //Setup for button
        GameObject button = new GameObject();
        button.name = buttonName;
        button.transform.parent = layoutGroupGameObject.transform;
        button.AddComponent<RectTransform>().sizeDelta = new Vector2 (150,50);
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