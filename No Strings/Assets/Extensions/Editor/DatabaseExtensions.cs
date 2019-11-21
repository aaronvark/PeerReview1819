using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Returns the MonoBehaviour's asset path
        /// </summary>
        public static string GetPath(this MonoBehaviour monoBehaviour)
        {
            var script = MonoScript.FromMonoBehaviour(monoBehaviour);
            return AssetDatabase.GetAssetPath(script);
        }

        /// <summary>
        /// Returns the ScriptableObject's asset path
        /// </summary>
        public static string GetPath(this ScriptableObject scriptableObject)
        {
            var script = MonoScript.FromScriptableObject(scriptableObject);
            return AssetDatabase.GetAssetPath(script);
        }

        /// <summary>
        /// Converts a directory path to an asset path where the starting directory is "Assets". returns the directory path if it cannot be converted.
        /// </summary>
        /// <param name="directoryPath">A directory path</param>
        public static string ToAssetPath(this string directoryPath)
        {
            string dataPath = Application.dataPath;

            // Check if the directory path can be converted to a valid Asset Path
            char c = directoryPath.ElementAtOrDefault(dataPath.Length);
            if(!directoryPath.Contains(dataPath) ||
                c != '\\' || c != '/')
                return directoryPath;

            return directoryPath.Remove(0, Application.dataPath.LastIndexOf("/Assets") + 1);
        }
    }
}
