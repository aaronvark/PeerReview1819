using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class SceneRequirementsTool
{
    public ToolWindow thisWindow;

    public void ShowSceneRequirementsTab(ScriptableObject _target)
    {
        GUILayout.Space(20);
        ScriptableObject target = _target;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty gameObjectsProperty = so.FindProperty("sceneRequirements");

  //      EditorGUILayout.PropertyField(gameObjectsProperty, true); // True means show children

        GUILayout.Label("Add an Object", EditorStyles.boldLabel);
        SerializedObject so2 = new SerializedObject(target);
        SerializedProperty AddObjects = so.FindProperty("addObject");

        EditorGUILayout.PropertyField(AddObjects, new GUIContent("Drag objects here"), false); // True means show children
      //  thisWindow.addObject = EditorGUILayout.ObjectField(thisWindow, true);


        if (thisWindow.addObject != null)
        {
          //  thisWindow.sceneRequirements.Add(thisWindow.addObject);
            foreach(GameObject obj in thisWindow.addObject)
            {
                thisWindow.sceneRequirements.Add(obj);
            }
            thisWindow.addObject = null;
        }

        GUILayout.Space(10f);

        // checks if there are already any assigned objects
        if (thisWindow.sceneRequirements.Count > 0)
        {
            GUILayout.Label("All Required Objects", EditorStyles.boldLabel);
            foreach (Object obj in thisWindow.sceneRequirements.ToArray())
            {
                if (obj != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                    GUI.color = thisWindow.RGBColor(191f, 63f, 63f);
                    if (GUILayout.Button("X"))
                    {
                        thisWindow.sceneRequirements.Remove(obj);
                    }
                    GUI.color = Color.white;
                    EditorGUILayout.EndHorizontal();

                    if (obj.GetType() != typeof(GameObject))
                    {
                        thisWindow.sceneRequirements.Remove(obj);
                    }
                }
                else
                {
                    thisWindow.sceneRequirements.Remove(obj);
                }
            }
        }
        GUI.color = thisWindow.RGBColor(127f, 191f, 63f);
        if (GUILayout.Button("Import Game Essentials"))
        {
            ImportGameAssets();
        }
        GUI.color = Color.white;
        so.ApplyModifiedProperties();
    }

    // Imports all the objects in the scene, also checks if the objects aren't already placed in the scene
    public void ImportGameAssets()
    {
        Vector3 _temp = new Vector3(0, 0, 0);
        foreach (GameObject obj in thisWindow.sceneRequirements.ToArray())
        {
            if (!GameObject.Find(obj.name))
            {
                GameObject _tempObj = GameObject.Instantiate(obj, _temp, Quaternion.Euler(_temp));
                _tempObj.name = obj.name;
            }
            else
            {
                Debug.Log("Object " + obj.name + " already exists!");
            }
        }
            Debug.Log("Objects have been placed!");
    }


    public void Load()
    {
        // Here we retrieve the data if it exists or we save the default field initialisers we set above
        var data = EditorPrefs.GetString("SceneRequirementsTool", JsonUtility.ToJson(this, false));
        // Then we apply them to this window
        JsonUtility.FromJsonOverwrite(data, this);
    }

    public void Save()
    {
        // We get the Json data
        var data = JsonUtility.ToJson(this, false);
        // And we save it
        EditorPrefs.SetString("SceneRequirementsTool", data);

       
    }
}
