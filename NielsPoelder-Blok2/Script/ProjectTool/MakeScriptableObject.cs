using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeScriptableObject : Editor
{
    [MenuItem("Assets/Create/ScriptableObjects/SpawnManagerScriptableObject")]
    public static ManagementObject CreateObject(string _name, ManagementStatus _status, ManagementType _type, string _description)
    {
        ManagementObject asset = ScriptableObject.CreateInstance<ManagementObject>();

        asset.itemName = _name;
        asset.itemStatus = _status;
        asset.itemType = _type;
        asset.itemDescription = _description;

        AssetDatabase.CreateAsset(asset, "Assets/ProjectManagement/" + _name + ".asset");
        AssetDatabase.SaveAssets();
        

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        return asset;
    }
}
