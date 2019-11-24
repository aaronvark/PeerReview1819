using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ProjectManagementTool
{
    public ToolWindow thisWindow;

    private int managementbarInt = 0;
    private string[] managementbarStrings = { "To Do", "Completed", "Create New" };


    private int orderTypeInt = 0;
    private string[] orderTypeString = { "All", "Mechanic", "Art", "Bug", "User Interface" };

    private Vector2 trelloScroll;

    private string tempItemName;
    private string tempItemDescription;
    private ManagementType tempItemManagementtype;

  
    public void ShowManagementTab(ScriptableObject _target)
    {

        ScriptableObject target = _target;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty gameObjectsProperty = so.FindProperty("managementObjects");

        EditorGUILayout.PropertyField(gameObjectsProperty, true); // True means show children


        GUILayout.Label("Trello Tool", EditorStyles.boldLabel);
        //thisWindow.addItem = EditorGUILayout.ObjectField(thisWindow.addItem, typeof(ManagementObject), true);

        GUILayout.Space(10);
        managementbarInt = GUILayout.Toolbar(managementbarInt, managementbarStrings);

       


      

        switch (managementbarInt)
        {
            case 0:
                if (thisWindow.managementObjects.Count > 0)
                {
                    GUILayout.Space(5);
                    orderTypeInt = GUILayout.Toolbar(orderTypeInt, orderTypeString);

                    trelloScroll = EditorGUILayout.BeginScrollView(trelloScroll);
                    foreach (ManagementObject obj in thisWindow.managementObjects.ToArray())
                    {

                        if (obj?.itemStatus == ManagementStatus.ToDo)
                        {
                            if (orderTypeInt == 0)
                            {
                                GetList(obj, true);
                            }
                            else if (orderTypeInt == 1 && obj?.itemType == ManagementType.Mechanic)
                            {
                                GetList(obj, true);
                            }
                            else if (orderTypeInt == 2 && obj?.itemType == ManagementType.Art)
                            {
                                GetList(obj, true);
                            }
                            else if (orderTypeInt == 3 && obj?.itemType == ManagementType.Bug)
                            {
                                GetList(obj, true);
                            }
                            else if (orderTypeInt == 4 && obj?.itemType == ManagementType.UI)
                            {
                                GetList(obj, true);
                            }
                        }

                    }
                    EditorGUILayout.EndScrollView();
                }
                break;

            case 1:
                if (thisWindow.managementObjects.Count > 0)
                {
                    foreach (ManagementObject obj in thisWindow.managementObjects.ToArray())
                    {
                        if (obj?.itemStatus == ManagementStatus.Completed)
                        {
                            GetList(obj, false);
                        }
                    }
                }
                break;

            case 2:
                // hier komt t deel waar je een nieuw object aan kan maken en toevoegen
                tempItemName = EditorGUILayout.TextField("Name", tempItemName);
                tempItemManagementtype = (ManagementType)EditorGUILayout.EnumPopup("Type", tempItemManagementtype);
                tempItemDescription = EditorGUILayout.TextField("Description", tempItemDescription);
                if (GUILayout.Button("ADD"))
                {


                    ManagementObject temp = MakeScriptableObject.CreateMyAsset(tempItemName, ManagementStatus.ToDo, tempItemManagementtype, tempItemDescription);

                    thisWindow.managementObjects.Add(temp);
                    tempItemName = "";
                    tempItemManagementtype = ManagementType.Art;
                    tempItemDescription = "";
                    so.ApplyModifiedProperties();
                }

                break;
            default:

                break;
        }
        so.ApplyModifiedProperties();
    }

    public void GetList(ManagementObject _obj, bool _showButtons)
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.Height(100));

        GUILayout.Label(_obj.itemName, EditorStyles.boldLabel);
        GUI.color = thisWindow.RGBColor(63, 127, 191);
        
        GUILayout.TextField(_obj.itemType.ToString(), EditorStyles.helpBox, GUILayout.Width(120));
        GUI.color = Color.white;
        GUILayout.Label(_obj.itemDescription, EditorStyles.textArea, GUILayout.ExpandWidth(false), GUILayout.Width(thisWindow.position.width - 230));

        GUILayout.EndVertical();
        if (_showButtons)
        {
            GUILayout.BeginVertical(GUILayout.Height(100));

            GUI.skin.button.fontSize = 24   ;
            GUI.skin.button.fontStyle = FontStyle.Bold;

            GUI.color = thisWindow.RGBColor(163, 44, 44); 
            if (GUILayout.Button("Remove", GUILayout.ExpandHeight(true), GUILayout.Width(200)))
            {
                string _temp = AssetDatabase.GetAssetPath(_obj);
                thisWindow.managementObjects.Remove(_obj);
                AssetDatabase.DeleteAsset(_temp);

            }

            GUI.color = thisWindow.RGBColor(67, 164, 67);
            if (GUILayout.Button("Complete",  GUILayout.ExpandHeight(true),  GUILayout.Width(200)))
            {
                _obj.itemStatus = ManagementStatus.Completed;
                AssetDatabase.SaveAssets();
            }

            GUI.skin.button.fontSize = 12;
            GUI.skin.button.fontStyle = FontStyle.Normal;
            GUI.color = Color.white;

            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
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

        // Et voilà !
    }


}
