using Extensions;
using System.CodeDom.Compiler;
using UnityEditor;
using UnityEngine;

namespace NoStrings
{
    public class PublicDisplayerWindow : EditorWindow
    {
        private PublicDisplayerSettings settings;
        private bool showTagSettings = false;
        private bool showLayerSettings = false;
        private bool settingsChanged = false;

        private readonly float singleLineHeight = EditorGUIUtility.singleLineHeight;
        private readonly float baseLabelWidth = 80f;

        [MenuItem("Window/Public Displayer/Settings", false, 2001)]
        private static void Init()
        {
            Vector2 windowMinSize = new Vector2(400, 200);
            Vector2 windowMaxSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            Vector2 startPosition = windowMaxSize / 2 + windowMinSize / 2;
            Rect start = new Rect(startPosition, windowMinSize);

            // Get existing open window or if none, make a new one:
            PublicDisplayerWindow window = GetWindowWithRect<PublicDisplayerWindow>(start, false, "Public Displayer Settings");
            window.minSize = windowMinSize;
            window.maxSize = windowMaxSize;
            window.Show();
        }

        private void OnEnable()
        {
            if (!PublicDisplayerSettings.GetSettings(out settings))
            {
                bool createSettingsFile = EditorUtility.DisplayDialog("Create Settings File?",
                    "A PublicDisplayerSettings file could not be found. Would you like to create one?" +
                    " If you do not create one, your settings can't be saved.",
                    "Yes",
                    "No");

                if (!createSettingsFile)
                {
                    settings = null;
                    return;
                }

                var script = MonoScript.FromScriptableObject(this);
                string path = AssetDatabase.GetAssetPath(script);
                string folderPath = path.Remove(path.LastIndexOf('/'));
                string settingsPath = folderPath + "/Settings.asset";

                AssetDatabase.CreateAsset(settings, settingsPath);
            }

            EditorUtility.SetDirty(settings);
        }

        private void OnDisable()
        {
            if(settingsChanged)
            {
                TagsWriter.ChangeTags();
                LayersWriter.ChangeLayers();
            }
        }

        private void OnGUI()
        {
            if(!settings)
            {
                this.Close();
                return;
            }

            EditorGUIUtility.labelWidth = baseLabelWidth;

            GUILayout.Label("Settings", EditorStyles.boldLabel);
            DrawPathSetting();
            DrawConversionMethodSetting();
            DrawAllSubSettings();

            settingsChanged |= GUI.changed;

            EditorGUIUtility.labelWidth = 0;
        }

        private void DrawPathSetting()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Path");

            // Draw the Folder Icon in a button that triggers a folder selection panel
            if(GUILayout.Button(EditorGUIUtility.IconContent("Folder Icon"),
                GUILayout.Width(singleLineHeight * 2), GUILayout.Height(singleLineHeight)))
            {
                string tempPath = EditorUtility.OpenFolderPanel("", "Assets", "");
                if(string.IsNullOrEmpty(tempPath))
                {
                    Debug.LogWarning($"Invalid Path: {tempPath}");
                }
                else
                {
                    // Change path and move assets
                    string oldPath = settings.Path;
                    settings.Path = tempPath;

                    CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
                    string fileExtension = provider.FileExtension;
                    provider.Dispose();

                    string tagsName = $"{settings.tagSettings.FileName}.{fileExtension}";
                    string layersName = $"{settings.layerSettings.FileName}.{fileExtension}";
                    AssetDatabase.MoveAsset($"{oldPath}/{tagsName}", $"{settings.Path}/{tagsName}");
                    AssetDatabase.MoveAsset($"{oldPath}/{layersName}", $"{settings.Path}/{layersName}");
                }
            }

            // Draw the path as a disabled textfield
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(settings.Path);//.Substring(settings.path.LastIndexOf("Assets")));
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }

        private void DrawConversionMethodSetting()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Convert to");
            settings.conversionMethod = (IdentifierConversion)EditorGUILayout.EnumPopup(settings.conversionMethod, MinWidth(" Camel Case ", EditorStyles.popup));
            string myString = "_My String\'s";
            string convertedString;
            switch(settings.conversionMethod)
            {
                case IdentifierConversion.CamelCase:
                    convertedString = myString.ToCamelCase();
                    break;
                case IdentifierConversion.PascalCase:
                    convertedString = myString.ToPascalCase();
                    break;
                default:
                    convertedString = myString.ToIdentifier();
                    break;
            }
            EditorGUILayout.LabelField($"{myString} = {convertedString}");
            EditorGUILayout.EndHorizontal();
        }

        private void DrawAllSubSettings()
        {
            showTagSettings = EditorGUILayout.Foldout(showTagSettings, "Tag Settings");
            if(showTagSettings)
            {
                EditorGUI.indentLevel++;
                DrawSubSettings(ref settings.tagSettings);
                EditorGUI.indentLevel--;
            }

            showLayerSettings = EditorGUILayout.Foldout(showLayerSettings, "Layer Settings");
            if(showLayerSettings)
            {
                EditorGUI.indentLevel++;
                DrawSubSettings(ref settings.layerSettings);
                EditorGUI.indentLevel--;
            }
        }

        private void DrawSubSettings(ref SubSettings subSettings)
        {
            subSettings.FileName = EditorGUILayout.DelayedTextField("File Name", subSettings.FileName);
        }

        private GUILayoutOption MinWidth(string text, GUIStyle style)
        {
            GUIContent content = new GUIContent(text);
            style.CalcMinMaxWidth(content, out float minWidth, out float maxWidth);
            return GUILayout.Width(minWidth);
        }
    }
}
