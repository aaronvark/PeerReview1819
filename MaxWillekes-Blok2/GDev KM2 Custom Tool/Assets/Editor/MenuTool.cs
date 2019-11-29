using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MenuTool : EditorWindow
{
    private const string nameString = "Menu Maker";
    private const string versionString = "V1.0.0";

    private string titelString = "Titel Placeholder";

    private string buttonStartString = "Start Name Placeholder";
    private string buttonQuitString = "Quit Name Placeholder";
    private string buttonString = "Button Name Placeholder";

    private List<string> buttonStringList = new List<string>();

    private Object buttonSprite;
    private Object backgroundSprite;

    private GameObject canvas;
    private GameObject layoutGroupGameObject;

    private string[] _choicesForButtons = new[] { "2", "3", "4", "5" };
    private int buttonAmountInt = 0;

    [MenuItem("Window/" + nameString + " " + versionString)]

    public static void ShowWindow()
    {
        GetWindow<MenuTool>($"{nameString} {versionString}");
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        titelString = EditorGUILayout.TextField("Titel", titelString);

        backgroundSprite = EditorGUILayout.ObjectField("Background image", backgroundSprite, typeof(Sprite), true);

        GUILayout.Label("Button Settings", EditorStyles.boldLabel);
        buttonAmountInt = EditorGUILayout.Popup(buttonAmountInt, _choicesForButtons);

        buttonStartString = EditorGUILayout.TextField("Start button", buttonStartString);
        buttonQuitString = EditorGUILayout.TextField("Quit button", buttonQuitString);

        int i = 0;

        while (i < buttonAmountInt)
        {
            if (buttonStringList.ElementAtOrDefault(i) == null)
            {
                buttonStringList.Add(EditorGUILayout.TextField("button" + (i + 1), buttonString));
            }
            else
            {
                buttonStringList[i] = EditorGUILayout.TextField("button" + (i + 1), buttonStringList[i]);
            }
            i++;
        }

        buttonSprite = EditorGUILayout.ObjectField("Button sprite", buttonSprite, typeof(Sprite), true);

        if (GUILayout.Button("Generate"))
        {
            GenerateMenu();
        }
    }

    void GenerateMenu()
    {
        canvas = new GameObject();
        layoutGroupGameObject = new GameObject();

        //Canvas and Event system.
        CustomCanvas Canvas = new CustomCanvas(canvas);

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