using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class NPCEditorWindow : EditorWindow
{
    public string LevelJsonString { get; set; }

    private Vector3 scrollPos = new Vector3(500, 0, 0);
    // declaring our serializable object, that we are working on
    public ScriptableNPC scriptableNpc;
    private SerializedObject serializedNpc;
    private SerializedProperty serializedNpcProperty;
    private ExtendedScriptableObjectDrawer extendedScriptableObjectDrawer;

    // Add menu named "NPCEditor" to the Window menu
    [MenuItem("Window/NPCEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        NPCEditorWindow window = (NPCEditorWindow)EditorWindow.GetWindow(typeof(NPCEditorWindow));
        window.Show();       
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("NPC:", EditorStyles.boldLabel);

        // Gets the property of our asset and create a field with its value
        scriptableNpc = EditorGUILayout.ObjectField(scriptableNpc, typeof(ScriptableNPC), true) as ScriptableNPC;
        
        if(GUILayout.Button("Edit This NPC"))
        {
            //serializedNpc = new SerializedObject(scriptableNpc);
            var editor = Editor.CreateEditor(scriptableNpc);
            if(editor != null)
            {
                editor.OnInspectorGUI();
            }
        }

        /*
        if (serializedNpc != null)
        {
            // Starting our manipulation
            // We're doing this before property rendering           
            serializedNpc.Update();
            serializedNpcProperty = serializedNpc.FindProperty("npcId");
            EditorGUILayout.PropertyField(serializedNpcProperty);
            // Apply changes
            serializedNpc.ApplyModifiedProperties();
        }*/


    }
}
