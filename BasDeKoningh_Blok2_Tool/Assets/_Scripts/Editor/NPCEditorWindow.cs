using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace EasyAI
{
    public class NPCEditorWindow : EditorWindow
    {
        public string LevelJsonString { get; set; }

        private Vector3 scrollPos = new Vector3(500, 0, 0);
        // declaring our serializable object, that we are working on
        public ScriptableNPC scriptableNpc;
        private SerializedObject serializedNpc;
        private SerializedProperty serializedNpcProperty;
        
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
            if (GUILayout.Button("Edit This NPC"))
            {

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

        private void DrawScriptableObject()
        {
            serializedNpc = new SerializedObject(scriptableNpc);
            foreach (SerializedProperty property in serializedNpc.GetIterator().GetChildren())
            {
                if (property.objectReferenceValue != null)
                {
                    property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, property.displayName, true);
                    EditorGUILayout.PropertyField(property, GUIContent.none, true);
                    if (GUI.changed) property.serializedObject.ApplyModifiedProperties();
                    if (property.objectReferenceValue == null) EditorGUIUtility.ExitGUI();

                    if (property.isExpanded)
                    {
                        // Draw a background that shows us clearly which fields are part of the ScriptableObject
                        //GUI.Box(new Rect(0, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing - 1, Screen.width, position.height - EditorGUIUtility.singleLineHeight - EditorGUIUtility.standardVerticalSpacing), "");

                        EditorGUI.indentLevel++;
                        var data = (ScriptableObject)property.objectReferenceValue;
                        SerializedObject serializedObject = new SerializedObject(data);

                        // Iterate over all the values and draw them
                        SerializedProperty prop = serializedObject.GetIterator();
                        float y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        if (prop.NextVisible(true))
                        {
                            do
                            {
                                // Don't bother drawing the class file
                                if (prop.name == "m_Script") continue;
                                float height = EditorGUI.GetPropertyHeight(prop, new GUIContent(prop.displayName), true);
                                EditorGUILayout.PropertyField(prop, true);
                                y += height + EditorGUIUtility.standardVerticalSpacing;
                            }
                            while (prop.NextVisible(false));
                        }
                        if (GUI.changed)
                            serializedObject.ApplyModifiedProperties();

                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    EditorGUILayout.ObjectField(property);
                    if (GUI.Button(new Rect(position.x + position.width - 58, position.y, 58, EditorGUIUtility.singleLineHeight), "Create"))
                    {
                        string selectedAssetPath = "Assets";
                        if (property.serializedObject.targetObject is MonoBehaviour)
                        {
                            MonoScript ms = MonoScript.FromMonoBehaviour((MonoBehaviour)property.serializedObject.targetObject);
                            selectedAssetPath = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(ms));
                        }
                        /*
                        System.Type type = fieldInfo.FieldType;
                        if (type.IsArray) type = type.GetElementType();
                        else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)) type = type.GetGenericArguments()[0];
                        property.objectReferenceValue = CreateAssetWithSavePrompt(type, selectedAssetPath);*/
                    }
                }
                property.serializedObject.ApplyModifiedProperties();
                EditorGUI.EndProperty();
            }
        }
    }
}
