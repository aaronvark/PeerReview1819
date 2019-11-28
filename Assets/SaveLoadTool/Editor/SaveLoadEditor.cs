using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Common.SaveLoadSystem
{

    [InitializeOnLoad]
    static class HierarchyIcons
    {
        static Texture2D texturePanel;
        static float hierarchyWindowWidth;

        static HierarchyIcons()
        {
            // Init
            texturePanel = AssetDatabase.LoadAssetAtPath("Assets/SaveLoadTool/save_icon.png", typeof(Texture2D)) as Texture2D; //TODO: find the right path
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItem;
        }

        static void HierarchyItem(int instanceID, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go != null)
            {
                if (go.GetComponent<SaveableIdentifier>())
                {
                    // place the icon to the right of the list:
                    Rect r = new Rect(selectionRect);
                    r.x = hierarchyWindowWidth;
                    r.width = 20;

                    GUI.Label(r, texturePanel);
                }
            } else
            {
                hierarchyWindowWidth = selectionRect.width;
            }
        }
    }

    static class SaveCommand
    {
        [MenuItem("GameObject/Saveable", false, 17)]
        static void MakeSaveable(MenuCommand command)
        {
            GameObject obj = (GameObject)command.context;
            Iidentifier identifier = obj.GetComponent<SaveableIdentifier>();

            if (identifier == null)
            {
                identifier = obj.AddComponent<SaveableIdentifier>();
                identifier.id = SaveLoadSystem.GetId();
                SaveSpecificEditor.ShowWindow(obj);
            } else
            {
                Object.DestroyImmediate((SaveableIdentifier)identifier);
                SaveLoadSystem.RemoveFromXML();
            }
        }

        [MenuItem("GameObject/Save specific", false, 17)]
        static void SaveWindow(MenuCommand command)
        {
            GameObject obj = (GameObject)command.context;
            SaveSpecificEditor.ShowWindow(obj);
        }

        [MenuItem("GameObject/Save specific", true)]
        static bool ValidateSaveWindow(MenuCommand command)
        {
            GameObject obj = (GameObject)command.context;
            return obj.GetComponent<SaveableIdentifier>() != null;
        }

        [MenuItem("Edit/Save all saveables &s", false, 1)]
        static void SaveObjectToXML()
        {
            SaveLoadSystem.ConvertAllObjectsToXML();
        }
    }
}