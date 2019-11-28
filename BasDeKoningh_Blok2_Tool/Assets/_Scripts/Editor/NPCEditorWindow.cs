using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using UnityEngine.Assertions;
namespace EasyAI
{
    public enum PresetType
    {
        ScriptablePreset = 0,
        Prefab = 1,
    }

    public class NPCEditorWindow : EditorWindow
    {
        //Input scriptable object
        public ScriptableObject InputScriptableObject;
        //Input current npc prefab
        private GameObject currentNPC;
        //List of the types we want to use
        private List<System.Type> settingTypes = new List<Type>();
        //Preset so we know what to draw and not
        private PresetType presetType = PresetType.ScriptablePreset;

        private readonly Dictionary<string, bool> checker = new Dictionary<string, bool>();

        private string jsonInput = string.Empty;

        // Add menu named "NPCEditor" to the Window menu
        [MenuItem("Window/NPCEditor #n")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            NPCEditorWindow window = (NPCEditorWindow)EditorWindow.GetWindow(typeof(NPCEditorWindow));
            window.Show();
        }

        void OnGUI()
        {

            presetType = (PresetType)EditorGUILayout.EnumPopup("Preset Type:", presetType);
            switch (presetType)
            {
                case PresetType.ScriptablePreset:
                    Repaint();
                    InputScriptableObject = EditorGUILayout.ObjectField(InputScriptableObject, typeof(ScriptableObject), true) as ScriptableObject;
                    break;
                case PresetType.Prefab:
                    Repaint();
                    currentNPC = EditorGUILayout.ObjectField(currentNPC, typeof(GameObject), true) as GameObject;
                    break;
                default:
                    break;
            }
            EditorGUILayout.LabelField("NPC:", EditorStyles.boldLabel);


            //TODO
            //Use assert to make system better 
            //Istead of item != null we should do if(item==null) assert
            if (InputScriptableObject != null)
            {
                var editor = Editor.CreateEditor(InputScriptableObject);
                if (editor != null)
                {
                    editor.OnInspectorGUI();
                }
                currentNPC = EditorGUILayout.ObjectField(currentNPC, typeof(GameObject), true) as GameObject;
                if (GUILayout.Button("Create This NPC"))
                {
                    //Create the npc with the selected settings
                    //We need to save the selected settings to maby some json file and we need to load the settings on the prefab. 
                    CreateNPC(InputScriptableObject as ScriptableNPC);
                }

                if (GUILayout.Button("Save Settings to Json"))
                {
                    SaveNPCSettings(InputScriptableObject as ScriptableNPC);
                }
            }

            if (currentNPC != null)
            {
                if (settingTypes == null || settingTypes.Count < 1)
                {
                    DrawNPCSettings(InputScriptableObject as ScriptableNPC);
                }
                //Future development
                /*
            EditorGUILayout.LabelField("Json Input: ", EditorStyles.boldLabel);
            if (GUILayout.Button("Load Json File"))
            {

                jsonInput = EditorUtility.OpenFilePanel("Load Json File", "/_Scripts/Json/", ".JSON");
                string scriptableObjectArea = string.Empty;

                scriptableObjectArea = File.ReadAllText(jsonInput);

                UnityEngine.Object[] inputs = JsonConverter<UnityEngine.Object>.FromJsonArray(scriptableObjectArea, "/SaveData.json");
                for(int cmpIndex = 0; cmpIndex < settingTypes.Count; cmpIndex++)
                {
                    var cmp = currentNPC.GetComponent(settingTypes[cmpIndex]);
                    cmp = inputs[cmpIndex] as Component;
                }
                Debug.Log(inputs[0]);
            }*/
                foreach (var type in settingTypes)
                {
                    if (!checker.ContainsKey(type.FullName))
                    {
                        bool show = false;
                        checker.Add(type.FullName, show);
                    }
                    if (GUILayout.Button(type.FullName))
                    {
                        foreach (var key in checker.Keys.ToList())
                        {
                            checker[key] = false;
                        }
                        checker[type.FullName] = true;
                    }
                    if (checker[type.FullName])
                    {
                        var component = currentNPC.GetComponent(type);
                        var settingEditor = Editor.CreateEditor(component);
                        if (settingEditor != null)
                        {
                            settingEditor.OnInspectorGUI();
                        }
                        // create style for the line
                        GUIStyle horizontalLine;
                        horizontalLine = new GUIStyle();
                        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
                        horizontalLine.margin = new RectOffset(0, 0, 4, 4);
                        horizontalLine.fixedHeight = 1;

                        // draw the line
                        HorizontalLine(Color.grey, horizontalLine);
                    }
                }
            }
            else
            {
                settingTypes = new List<Type>();
            }
        }

        // utility method to create a line
        private void HorizontalLine(Color color, GUIStyle line)
        {
            var c = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, line);
            GUI.color = c;
        }

        private void SaveNPCSettings(ScriptableNPC npc)
        {
            string jsonString = "";
            List<string> settings = new List<string>();
            string scriptableNpc = string.Empty;
            scriptableNpc = JsonUtility.ToJson(npc);
            settings.Add(scriptableNpc);
            foreach (var setting in npc.settings)
            {
                System.Type settingType = setting.ObjectToClassType();
                string settingJson = string.Empty;
                settingJson = JsonUtility.ToJson(currentNPC.GetComponent(settingType));
                settings.Add(settingJson);

            }
            if (settings.Count < 1 || settings == null) return;
            jsonString = JsonConverter<string>.SerializeToJson(settings, jsonString, "/_Scripts/Json/SaveData.json");
            Debug.Log(jsonString);
        }

        private void DrawNPCSettings(ScriptableNPC npc)
        {
            try
            {
                foreach (var setting in npc.settings)
                {
                    settingTypes.Add(setting.ObjectToClassType());
                }
            }
            catch(Exception e)
            {
                //Debug.Log(settingTypes);
            }
        }

        private void CreateNPC(ScriptableNPC npc)
        {
            settingTypes.Clear();
            GameObject newNpc = Instantiate(npc.Model, npc.SpawnPosition.position, Quaternion.identity);

            //Component script = newNpc.AddComponent(npc.AISystem.ObjectToClassType());
            foreach(var setting in npc.settings)
            {
                System.Type settingType = setting.ObjectToClassType();
                newNpc.AddComponent(settingType);
                settingTypes.Add(settingType);
            }
            SetCurrentNPC(newNpc);
            //Let's see if the aisystem component is my AISystem
            try
            {
                newNpc.name = npc.NpcName;
                if(newNpc.GetComponent<Animator>() == null)
                {
                    var npcAnim = newNpc.AddComponent<Animator>();
                    npcAnim.applyRootMotion = false;
                }
                newNpc.GetComponent<Animator>().runtimeAnimatorController = npc.AnimatorController;
            }
            catch(Exception e)
            {
                Debug.Log("It's not an AiSystem created with the reguired methods");
            }
        }

        public int DebugNPCCreation()
        {
            Assert.IsNotNull(InputScriptableObject);
            ScriptableNPC newNPC = new ScriptableNPC();
            newNPC = InputScriptableObject as ScriptableNPC;
            newNPC.AnimatorController = null;
            
            CreateNPC(newNPC);

            Assert.AreEqual(InputScriptableObject, newNPC);
            return 0;
        }

        private void SetCurrentNPC(GameObject setter)
        {
            if (currentNPC != null) return;
            currentNPC = setter;
        }
    }
}
