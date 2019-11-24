using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace WorldBuilderTool
{
    [System.Serializable]
    public class EasyWorldBuilderEditor : EditorWindow, UnityEngine.ISerializationCallbackReceiver
    {
        WorldBuilderSettings settings; //Scriptable SaveFile loaded from Resources/EasyWorldBuilder

        #region AssetList Serelization
        [SerializeField] public List<GameObject> AssetListCopy = new List<GameObject>();

        public void OnBeforeSerialize()
        {
            AssetListCopy = settings.AssetList;
        }

        public void OnAfterDeserialize()
        {
            settings.AssetList = AssetListCopy;
        }
        #endregion

        //UI
        Texture2D backgroundTexture;
        Texture2D assetBackground;
        Rect backgroundSection;
        Rect assetSection;

        static EasyWorldBuilder runtimeEditor;

        PlacementMode placementMode;

        [MenuItem("EasyWorldBuilder/EasyWorldBuilder Editor")]
        public static void DrawWindow()
        {
            EasyWorldBuilderEditor window = GetWindow<EasyWorldBuilderEditor>("Easy WorldBuilder");
            window.minSize = new Vector2(300, 300);
            window.Show();
            EditorUtility.SetDirty(window);
        }

        private void OnEnable()
        {
            if (Resources.Load<WorldBuilderSettings>("EasyWorldBuilder/EasyWorldBuilder_Settings") == null)
            {
                WorldBuilderSettings settings = ScriptableObject.CreateInstance<WorldBuilderSettings>();

                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/EasyWorldBuilder/EasyWorldBuilder_Settings.asset");

                AssetDatabase.CreateAsset(settings, assetPathAndName);
                AssetDatabase.SaveAssets();
            }

            settings = Resources.Load<WorldBuilderSettings>("EasyWorldBuilder/EasyWorldBuilder_Settings");
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

                    runtimeEditor.settings = settings;
                    runtimeEditor.SpawnController(placementMode);
                    break;

                case PlayModeStateChange.ExitingPlayMode:
                    if (FindObjectOfType<EasyWorldBuilder>() != null)
                    {
                        Destroy(runtimeEditor.gameObject);
                    }
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
            GUILayout.Label("\nPlacement Settings \n", EditorStyles.boldLabel);

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
                    runtimeEditor.controller.curMode = placementMode;
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

            if (settings.AssetList.Count > 0)
            {
                for (int i = 0; i < settings.AssetList.Count; i++)
                {
                    //Debug.Log(AssetList.Count);
                    settings.AssetList[i] = (GameObject)EditorGUILayout.ObjectField(settings.AssetList[i], typeof(GameObject), false);
                }
            }

            //Extra whitespace
            GUILayout.Label("");
        }

        void DrawKeybindings()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Label("Keybindings", EditorStyles.boldLabel);

            GUILayout.Label("Placement", EditorStyles.miniBoldLabel);
            settings.placeButton = (KeyCode)EditorGUILayout.EnumPopup("Confirm Placement", settings.placeButton);


            GUILayout.Label("Rotation", EditorStyles.miniBoldLabel);
            settings.rotLeft = (KeyCode)EditorGUILayout.EnumPopup("Rotate Asset Left", settings.rotLeft);

            settings.rotRight = (KeyCode)EditorGUILayout.EnumPopup("Rotate Asset Right", settings.rotRight);

            settings.rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", settings.rotationSpeed);

           
            GUILayout.Label("Scaling", EditorStyles.miniBoldLabel);
            settings.ScaleAxis = EditorGUILayout.TextField("Scaling Axis", settings.ScaleAxis);
            settings.scaleSpeed = EditorGUILayout.FloatField("Scaling Speed", settings.scaleSpeed);

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
                runtimeEditor.controllerUI.addButton(null);
            }
        }    

        void RemoveFromList()
        {
            if (EditorApplication.isPlaying == true)
            {
                runtimeEditor.controllerUI.removeButton();
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
