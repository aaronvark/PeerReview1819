using Extensions;
using UnityEditor;
using UnityEngine;

namespace PublicDisplayer
{
    public class PublicDisplayerSettings : ScriptableObject
    {
        private static string currentPath = null;

        [SerializeField]
        private string path = null;
        public IdentifierConversion conversionMethod;
        public SubSettings tagSettings = new SubSettings("Tags");
        public SubSettings layerSettings = new SubSettings("Layers");
        public ControllerSettings controllerSettings = new ControllerSettings("Controllers");

        public string Path
        {
            get
            {
                if(path == null)
                    path = Application.dataPath.Remove(0, Application.dataPath.LastIndexOf("/Assets") + 1);
                return path;
            }
            set
            {
                if(value.Length < Application.dataPath.Length)
                    value = Application.dataPath;

                path = value.Remove(0, Application.dataPath.LastIndexOf("/Assets") + 1);
            }
        }

        public static bool GetSettings(out PublicDisplayerSettings settings)
        {
            // Get settings from the cached path.
            settings = AssetDatabase.LoadAssetAtPath<PublicDisplayerSettings>(currentPath);
            if(settings)
                return true;

            // If the cached path is incorrect, the file has been moved and we need to search for it again.
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(PublicDisplayerSettings).Name}");

            // If we can't find any file, return a generic instance instead...
            if(guids.Length <= 0)
            {
                settings = CreateInstance<PublicDisplayerSettings>();
                return false;
            }
            // ...or log a warning saying we found too many files.
            else if(guids.Length > 1)
            {
                Debug.LogWarning("More than 1 PublicDisplayerSetting assets were found. " +
                    "Only the first one will be used. " +
                    "We recommend deleting all surplus assets.");
            }

            // Get the settings from the filepath.
            string settingsPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            settings = AssetDatabase.LoadAssetAtPath<PublicDisplayerSettings>(settingsPath);
            currentPath = settingsPath;

            return true;
        }
    }
}
