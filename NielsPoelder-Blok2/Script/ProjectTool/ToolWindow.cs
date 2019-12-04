using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolWindow : EditorWindow
{
    public Object[] addObject;
    public List<Object> sceneRequirements = new List<Object>();

    public Object addItem;
    public List<ManagementObject> managementObjects = new List<ManagementObject>();

    int toolbarInt = 0;
    string[] toolbarStrings = { "Scene Requirements", "Project Management" };

   

    SceneRequirementsTool RequirementTool = new SceneRequirementsTool();
    
    ProjectManagementTool ManagementTool = new ProjectManagementTool();

    [MenuItem("Window/The “Swiss” Project Utility Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ToolWindow>("The “Swiss” Project Utility Tool");
        
    }


    void OnGUI()
    {
        RequirementTool.thisWindow = this;
        ManagementTool.thisWindow = this;

        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);

        switch (toolbarInt)
        {
            case 0:
                RequirementTool.ShowSceneRequirementsTab(this);
                break;

            case 1:
                ManagementTool.ShowManagementTab(this);
                break;

            default:
                RequirementTool.ShowSceneRequirementsTab(this);
                break;
        }

    }

    // simple script that normalized RGB colors to code colors
    public Color RGBColor(float _r, float _g, float _b)
    {
        return new Color((_r / 255f), (_g / 255f), (_b / 255f), 1f);
    }

    // loads all data and finds all the objects
    protected void OnEnable()
    {

        RequirementTool.Load();
        ManagementTool.Load();
        // Here we retrieve the data if it exists or we save the default field initialisers we set above
        var data = EditorPrefs.GetString("ToolWindow", JsonUtility.ToJson(this, false));
        // Then we apply them to this window
        JsonUtility.FromJsonOverwrite(data, this);

        managementObjects.Clear();
        string[] guids1 = AssetDatabase.FindAssets("t:ManagementObject", null);
        foreach (string guid1 in guids1)
        {

            ManagementObject temp = (ManagementObject)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid1), typeof(ManagementObject));
            foreach (ManagementObject obj in managementObjects)
            {
                if (temp == obj)
                    break;
            }
             managementObjects.Add(temp);
        }

    }

    protected void OnDisable()
    {
        RequirementTool.Save();
        ManagementTool.Save();
        AssetDatabase.SaveAssets();
        // We get the Json data
        var data = JsonUtility.ToJson(this, false);
        // And we save it
        EditorPrefs.SetString("ToolWindow", data);

        // Et voilà !
    }

}
