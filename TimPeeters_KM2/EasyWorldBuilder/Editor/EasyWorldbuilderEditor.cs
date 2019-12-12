using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WorldBuilderTool
{
    public class EasyWorldBuilderEditor : EditorWindow
    {
        private WorldBuilderSettings settings; //Scriptable SaveFile loaded from Resources/EasyWorldBuilder

        //UI
        private Texture2D backgroundTexture;
        private Texture2D assetBackground;
        private Rect backgroundSection;
        private Rect assetSection;
        private Vector2 scrollPos;

        private static EasyWorldBuilder runtimeEditor;
        private PlacementMode placementMode;
        
        [MenuItem("EasyWorldBuilder/EasyWorldBuilder Editor")]
        public static void DrawWindow()
        {
            EasyWorldBuilderEditor window = GetWindow<EasyWorldBuilderEditor>("Easy WorldBuilder");
            window.minSize = new Vector2(400, 450);
            window.Show();
            EditorUtility.SetDirty(window);
        }

        private void OnEnable()
        {
            settings = Resources.Load<WorldBuilderSettings>("EasyWorldBuilder/EasyWorldBuilder_Settings");
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<WorldBuilderSettings>();

                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/EasyWorldBuilder/EasyWorldBuilder_Settings.asset");

                AssetDatabase.CreateAsset(settings, assetPathAndName);
                AssetDatabase.SaveAssets();
            }

            EditorUtility.SetDirty(settings);

            InitTextures();

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    if (FindObjectOfType<EasyWorldBuilder>() == null)
                    {
                        runtimeEditor = new GameObject().AddComponent<EasyWorldBuilder>();
                        runtimeEditor.gameObject.name = "WorldBuilder Runtime Editor";
                        runtimeEditor.hideFlags = HideFlags.HideInHierarchy;
                    }

                    runtimeEditor.Settings = settings;
                    runtimeEditor.SpawnController(placementMode);
                    break;

                case PlayModeStateChange.ExitingPlayMode:
                    //Save Assets on playmode exit
                    runtimeEditor.SaveSys.SavePlacedAssets();

                    if (FindObjectOfType<EasyWorldBuilder>() != null)
                    {
                        Destroy(runtimeEditor.gameObject);
                    }
                    break;
                case PlayModeStateChange.EnteredEditMode:
                    //Load Assets on editmode enter
                    new LoadFromFile().LoadData();
                    break;
            }
        }

        private void StartRuntime()
        {
            EditorApplication.isPlaying = true;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDestroy()
        {
            if (FindObjectOfType<EasyWorldBuilder>() != null)
            {
                DestroyImmediate(runtimeEditor.gameObject);
            }

            AssetDatabase.SaveAssets();
        }

        private void OnGUI()
        {
            DrawTextures();
            GUI.DrawTexture(backgroundSection, backgroundTexture);

            GUILayout.BeginArea(backgroundSection);
            GUIStyle style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 36, fontStyle = FontStyle.Bold, fixedHeight = 60f };
            GUILayout.Label("Easy Worldbuilder ", style);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            //Editmode Button
            placementMode = (PlacementMode)EditorGUILayout.EnumPopup("Placement Mode: ", placementMode);
            GUILayout.Label("");

            DrawAssetList();

            DrawKeybindings();

            GUILayout.EndVertical();

            GUILayout.EndArea();

            //Mode of asset placement can be changed realtime. (Flying or FPS)
            if (EditorApplication.isPlaying)
            {
                try
                {
                    runtimeEditor.Controller.CurrentMode = placementMode;
                }
                catch
                {
                }

            }
        }

        //Draws buttons and objectfields for placeable assets.
        void DrawAssetList()
        {
            GUILayout.Label("Asset List", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", GUILayout.Height(20)))
            {
                AddToList();
            }

            if (GUILayout.Button("-", GUILayout.Height(20)) && settings.AssetList.Count > 0)
            {
                RemoveFromList();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(Screen.width - 10f), GUILayout.Height(100));
            if (settings.AssetList.Count > 0)
            {
                for (int i = 0; i < settings.AssetList.Count; i++)
                {
                    //Debug.Log(AssetList.Count);
                    settings.AssetList[i] = (GameObject)EditorGUILayout.ObjectField(settings.AssetList[i], typeof(GameObject), false);
                }
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            //Extra whitespace
            GUILayout.Label("");
        }

        void DrawKeybindings()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Keybindings", EditorStyles.boldLabel);

            GUILayout.Label("Placement", EditorStyles.miniBoldLabel);
            settings.PlaceButton = (KeyCode)EditorGUILayout.EnumPopup("Confirm Placement", settings.PlaceButton);
            settings.PivotSnap = EditorGUILayout.Toggle("Snap Object to Pivot", settings.PivotSnap);
            settings.SnapGrid = EditorGUILayout.Toggle("Snap To Grid", settings.SnapGrid);
            settings.GridSize = EditorGUILayout.Vector3Field("Grid Size", settings.GridSize);


            GUILayout.Label("Rotation", EditorStyles.miniBoldLabel);
            settings.RotLeft = (KeyCode)EditorGUILayout.EnumPopup("Rotate Asset Left", settings.RotLeft);

            settings.RotRight = (KeyCode)EditorGUILayout.EnumPopup("Rotate Asset Right", settings.RotRight);

            settings.RotationSpeed = EditorGUILayout.FloatField("Rotation Speed", settings.RotationSpeed);

           
            GUILayout.Label("Scaling", EditorStyles.miniBoldLabel);
            settings.ScaleAxis = EditorGUILayout.TextField("Scaling Axis", settings.ScaleAxis);
            settings.ScaleSpeed = EditorGUILayout.FloatField("Scaling Speed", settings.ScaleSpeed);


            EditorGUILayout.EndVertical();

            /*
            //Enables runtime editing of keybindings.
            if (EditorApplication.isPlaying)
            {
                try
                {
                    runtimeEditor.placeButton = settings.placeButton;
                    runtimeEditor.rotLeft = settings.rotLeft;
                    runtimeEditor.rotRight = settings.rotRight;
                }
                catch { }
            }
            */
        }

        void AddToList()
        {
            settings.AssetList.Add(null);   

            if(EditorApplication.isPlaying == true)
            {
                runtimeEditor.ControllerUI.addButton(null);
            }
        }    

        void RemoveFromList()
        {
            if (EditorApplication.isPlaying == true)
            {
                runtimeEditor.ControllerUI.removeButton();
            }

            settings.AssetList.RemoveAt(settings.AssetList.Count - 1);
        }

        void InitTextures()
        {
            backgroundTexture = new Texture2D(1, 1);
            assetBackground = new Texture2D(1, 1);

            Color bgColor = new Color(55 / 255f, 55 / 255f, 55 / 255f, 0.4f);
            backgroundTexture.SetPixel(0, 0, bgColor);
            backgroundTexture.Apply();

            assetBackground.Apply();
        }

        void DrawTextures()
        {
            backgroundSection.x = 0;
            backgroundSection.y = 0;
            backgroundSection.width = Screen.width;
            backgroundSection.height = Screen.height;
        }
    }
}
