using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Common.SaveLoadSystem
{

    [InitializeOnLoad]
    static class MyHierarchyIcons
    {
        static Texture2D texturePanel;
        static float hierarchyWindowWidth;

        static MyHierarchyIcons()
        {
            // Init
            texturePanel = AssetDatabase.LoadAssetAtPath("Assets/SaveLoadTool/save_icon.png", typeof(Texture2D)) as Texture2D;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        static void HierarchyItemCB(int instanceID, Rect selectionRect)
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
            } else
            {
                Object.DestroyImmediate((SaveableIdentifier)identifier);
            }
        }

        [MenuItem("GameObject/Save specific", false, 17)]
        static void SaveSpecific(MenuCommand command)
        {
            GameObject obj = (GameObject)command.context;
            SaveSpecificEditor.ShowWindow(obj);
        }
    }
}