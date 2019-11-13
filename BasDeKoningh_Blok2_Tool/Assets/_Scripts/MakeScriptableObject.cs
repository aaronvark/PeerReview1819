using UnityEngine;
using UnityEditor;
namespace EasyAI
{
    public class MakeScriptableObject
    {
#if UNITY_EDITOR

        [MenuItem("Assets/Create/ScriptableNPC")]
        public static void CreateMyNpc()
        {
            ScriptableNPC asset = ScriptableObject.CreateInstance<ScriptableNPC>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/NewScriptableNPC.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        [MenuItem("Assets/Create/ScriptableSetting")]
        public static void CreateMySetting()
        {
            ScriptableSetting asset = ScriptableObject.CreateInstance<ScriptableSetting>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/Settings/NewScriptableSetting.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

#endif

    }
}