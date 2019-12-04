using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ProjectManagementTool
{
    public ToolWindow thisWindow;

    private int managementbarInt = 0;
    private string[] managementbarStrings = { "Working on", "Completed", "Create New" };


    private int orderTypeInt = 0;
    private string[] orderTypeString = { "All", "Mechanic", "Art", "Bug", "User Interface" };

    private Vector2 trelloScroll;

    private string tempItemName;
    private string tempItemDescription;
    private ManagementType tempItemManagementtype;

    private ManagementObject editObject;

    public void ShowManagementTab(ScriptableObject _target)
    {

        ScriptableObject target = _target;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty gameObjectsProperty = so.FindProperty("managementObjects");

     //   EditorGUILayout.PropertyField(gameObjectsProperty, true); // True means show children


        //thisWindow.addItem = EditorGUILayout.ObjectField(thisWindow.addItem, typeof(ManagementObject), true);

        GUILayout.Space(20);
        managementbarInt = GUILayout.Toolbar(managementbarInt, managementbarStrings);

       


      

        switch (managementbarInt)
        {
            // displayed alle scriptable objects
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
                            // Saves ordening for the user of the tool
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
                // displayed alle voltooide scriptable objects
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
                // displayed het tab waar je de scriptable objects kan aanmaken
            case 2:
                tempItemName = EditorGUILayout.TextField("Task Name", tempItemName);
                tempItemManagementtype = (ManagementType)EditorGUILayout.EnumPopup("Task Type", tempItemManagementtype);
                tempItemDescription = EditorGUILayout.TextField("Task Description", tempItemDescription);
                if (GUILayout.Button("ADD"))
                {
                    foreach(char i in tempItemName)
                    {
                        // checkt of er een "illegaal" character (/) in de naam zit en verwijdert deze voor de path
                        if (i.ToString() == "/")
                        {
                            tempItemName.Remove(i);
                        }
                    }



                    ManagementObject temp = MakeScriptableObject.CreateObject(tempItemName, ManagementStatus.ToDo, tempItemManagementtype, tempItemDescription);

                    thisWindow.managementObjects.Add(temp);
                    tempItemName = "";
                    tempItemManagementtype = ManagementType.Art;
                    tempItemDescription = "";
                    so.ApplyModifiedProperties();
                }

                break;
                // displayed het tab waar de scriptable objects kunnen worden geëdit
            case 3:
                
                GUILayout.Label("Edit Object", EditorStyles.boldLabel);
                
                editObject.itemName = EditorGUILayout.TextField("Task Name", editObject.itemName);

                editObject.itemType = (ManagementType)EditorGUILayout.EnumPopup("Task Type", editObject.itemType);

                editObject.itemDescription = EditorGUILayout.TextField("Task Description", editObject.itemDescription);


                GUI.skin.button.fontSize = 14;
                GUI.skin.button.fontStyle = FontStyle.Bold;
                GUI.color = thisWindow.RGBColor(127f, 191f, 63f);
                if (GUILayout.Button("Complete Edit"))
                {
                    // Slaat de wijzigingen op en stuurt de gebruiker terug naar het main view tab.
                    managementbarInt = 0;
                    EditorUtility.SetDirty(editObject);
                    AssetDatabase.SaveAssets();
                }


                GUI.skin.button.fontSize = 12;
                GUI.skin.button.fontStyle = FontStyle.Normal;
                GUI.color = Color.white;

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


        EditorGUILayout.BeginHorizontal();

        GUILayout.Label(_obj.itemName, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Task Type", EditorStyles.boldLabel);
        GUI.color = thisWindow.RGBColor(63, 127, 191);
        GUILayout.TextField(_obj.itemType.ToString(), EditorStyles.helpBox, GUILayout.Width(120)); // haalt de task naam op
        GUI.color = Color.white;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Task Description", EditorStyles.boldLabel);     
        //GUILayout.Label(_obj.itemDescription, EditorStyles.textArea, GUILayout.ExpandWidth(false), GUILayout.Width(thisWindow.position.width - 370));
        GUILayout.TextField(_obj.itemDescription, EditorStyles.textArea, GUILayout.ExpandWidth(false), GUILayout.Width(thisWindow.position.width - 380)); // haalt de task descriptie op
        EditorGUILayout.EndHorizontal();


        GUILayout.EndVertical();
        if (_showButtons)
        {
            GUILayout.BeginVertical(GUILayout.Height(100));

            GUI.skin.button.fontSize = 14   ;
            GUI.skin.button.fontStyle = FontStyle.Bold;

            GUI.color = thisWindow.RGBColor(191f, 63f, 63f); 
            if (GUILayout.Button("Remove", GUILayout.ExpandHeight(true), GUILayout.Width(200)))
            {
                // Verwijdert het object en uit de file database
                string _temp = AssetDatabase.GetAssetPath(_obj);
                thisWindow.managementObjects.Remove(_obj);
                AssetDatabase.DeleteAsset(_temp);

            }

            GUI.color = thisWindow.RGBColor(127f, 191f, 63f);
            if (GUILayout.Button("Complete",  GUILayout.ExpandHeight(true),  GUILayout.Width(200)))
            {
                // zet het object op complete en slaat de wijzigingen op
                _obj.itemStatus = ManagementStatus.Completed;
                EditorUtility.SetDirty(_obj);
                AssetDatabase.SaveAssets();
            }

            GUI.color = thisWindow.RGBColor(47f, 165f, 255f);
            if (GUILayout.Button("Edit", GUILayout.ExpandHeight(true), GUILayout.Width(200)))
            {
                // als je heir op klikt dan wordt editable object dit object
                managementbarInt = 3;
                editObject = _obj;
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

    }


}
