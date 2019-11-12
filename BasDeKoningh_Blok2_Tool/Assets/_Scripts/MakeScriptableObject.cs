using UnityEngine;
using UnityEditor;
namespace EasyAI
{
    public class MakeScriptableObject
    {
#if UNITY_EDITOR

        [MenuItem("Assets/Create/ScriptableNPC")]
        public static void CreateMyAsset()
        {
            ScriptableNPC asset = ScriptableObject.CreateInstance<ScriptableNPC>();

            AssetDatabase.CreateAsset(asset, "Assets/Resources/NewScriptableNPC.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

#endif

    }
}