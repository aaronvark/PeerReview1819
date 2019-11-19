using System.Collections.Generic;
using Dialogue;
using UnityEditor;
using UnityEngine;

namespace DialogueEditor {
[CustomEditor(typeof(DialogueTrigger))]
public class TriggerInspector : Editor {
    private readonly List<string> options = new List<string>();
    private int choiceIndex;

    public override void OnInspectorGUI() {
        var trigger = (DialogueTrigger) target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerActive"));

        choiceIndex = EditorGUILayout.Popup("Trigger button/axis", choiceIndex, options.ToArray());
        trigger.triggerButton = options[choiceIndex];

        GUILayout.Space(10);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueGraph"));

        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable() {
        LoadInputButtons();
    }

    /// <summary>
    /// Loads/updates the list of available input buttons from the InputManager in Unity.
    /// </summary>
    private void LoadInputButtons() {
        // Code stolen from https://answers.unity.com/questions/951770/get-array-of-all-input-manager-axes.html
        var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
        var obj = new SerializedObject(inputManager);
        var axisArray = obj.FindProperty("m_Axes");

        if (axisArray.arraySize == 0) {
            Debug.Log("No Axes");
            return;
        }

        options.Clear();

        // Add the option to use something else to activate the button //TODO rework this to have a toggle button
        options.Add(DialogueTrigger.DO_NOT_USE);

        for (var i = 0; i < axisArray.arraySize; ++i) {
            // Get the name of the input button and add it to the list
            var axis = axisArray.GetArrayElementAtIndex(i);
            options.Add(axis.FindPropertyRelative("m_Name").stringValue);
        }

        // Obtain the currently selected index from the underlying DialogueTrigger. Retains selection throughout
        // inspector opening/closing.
        var trigger = (DialogueTrigger) target;
        choiceIndex = options.IndexOf(trigger.triggerButton);
    }
}
}
