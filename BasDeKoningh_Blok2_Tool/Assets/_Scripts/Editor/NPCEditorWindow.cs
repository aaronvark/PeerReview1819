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
        public ScriptableObject scriptableObject;
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
            EditorGUILayout.LabelField("NPC:", EditorStyles.boldLabel);

            // Gets the property of our asset and create a field with its value
            scriptableObject = EditorGUILayout.ObjectField(scriptableObject, typeof(ScriptableObject), true) as ScriptableObject;
            if(scriptableObject == null) { return; }
            var editor = Editor.CreateEditor(scriptableObject);
            if (editor != null)
            {
                editor.OnInspectorGUI();
            }
            if (GUILayout.Button("Create This NPC"))
            {
                //Create the npc with the selected settings
                //We need to save the selected settings to maby some json file and we need to load the settings on the prefab. 
                CreateNPC(scriptableObject as ScriptableNPC);
            }
        }

        private void CreateNPC(ScriptableNPC npc)
        {
            GameObject newNpc = Instantiate(npc.Prefab, npc.SpawnPosition.position, Quaternion.identity);
            var script = newNpc.AddComponent<AISystem>();
            foreach(var setting in npc.settings)
            {
                Debug.Log(setting);
                MonoScript monoScript = setting as MonoScript;
                System.Type settingType = monoScript.GetClass();
                newNpc.AddComponent(settingType);
            }
            script.GiveNpcData(npc);
            script.InitAI();
            //We still need to make a change so we can save the selected properties on the npc
            //However what happens now is that it overrides the selected settings scriptable objects

        }
    }
}
