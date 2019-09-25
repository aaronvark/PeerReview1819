using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class HiddenObject
{
    public GameObject HiddenGameObject;
}

public class LevelEditorWindow : EditorWindow
{
    public string LevelJsonString { get; set; }
    public List<Level> levels;

    public GameObject ObjectPlaceholder;
    public HiddenObject hiddenObject;

    private Vector3 scrollPos = new Vector3(500, 0, 0);
    private Level placeHolderLevel = new Level();

    //private bool show = false;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/LevelEditorWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("LoadJson"))
        {
            FromJson();
        }
        if (GUILayout.Button("SaveJson"))
        {
            SerializeToJson();
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("Level Management", EditorStyles.boldLabel);
        GUILayout.Label("Default Add Level", EditorStyles.boldLabel);
        GUILayout.Label("Level Enemy Amounts: ", EditorStyles.boldLabel);
        placeHolderLevel.EnemyAmounts = EditorGUILayout.IntField(placeHolderLevel.EnemyAmounts);
        GUILayout.Label("Level Enemies Alive: ", EditorStyles.boldLabel);
        placeHolderLevel.EnemiesAlive = EditorGUILayout.IntField(placeHolderLevel.EnemiesAlive);
        GUILayout.Label("Level Done: ", EditorStyles.boldLabel);
        placeHolderLevel.Done = EditorGUILayout.Toggle(placeHolderLevel.Done);
        GUILayout.Label("Next Level Player Position: ", EditorStyles.boldLabel);
        placeHolderLevel.NextLevelPosition = EditorGUILayout.Vector3Field("NextLevelPosition:",placeHolderLevel.NextLevelPosition);
        GUILayout.Label("Current Level Player Position: ", EditorStyles.boldLabel);
        placeHolderLevel.CurrentLevelPostion = EditorGUILayout.Vector3Field("CurrentLevelPosition:", placeHolderLevel.CurrentLevelPostion);
        GUILayout.Label("Level Prefab: ", EditorStyles.boldLabel);
        placeHolderLevel.LevelPrefab = EditorGUILayout.ObjectField(placeHolderLevel.LevelPrefab, typeof(GameObject), true) as GameObject;
        if (GUILayout.Button("Add Level"))
        {
            AddLevel();
        }
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(500), GUILayout.Height(600));
        if (levels != null)
        {
            foreach (Level level in levels)
            {
                EditorGUI.indentLevel = 0;              
                GUILayout.Label("Level Enemy Amounts: ", EditorStyles.boldLabel);
                placeHolderLevel.EnemyAmounts = EditorGUILayout.IntField(placeHolderLevel.EnemyAmounts);
                GUILayout.Label("Level Enemies Alive: ", EditorStyles.boldLabel);
                placeHolderLevel.EnemiesAlive = EditorGUILayout.IntField(placeHolderLevel.EnemiesAlive);
                GUILayout.Label("Level Done: ", EditorStyles.boldLabel);
                placeHolderLevel.Done = EditorGUILayout.Toggle(placeHolderLevel.Done);
                GUILayout.Label("Next Level Player Position: ", EditorStyles.boldLabel);
                placeHolderLevel.NextLevelPosition = EditorGUILayout.Vector3Field("NextLevelPosition:", placeHolderLevel.NextLevelPosition);
                GUILayout.Label("Current Level Player Position: ", EditorStyles.boldLabel);
                placeHolderLevel.CurrentLevelPostion = EditorGUILayout.Vector3Field("CurrentLevelPosition:", placeHolderLevel.CurrentLevelPostion);
                GUILayout.Label("Level Prefab: ", EditorStyles.boldLabel);
                placeHolderLevel.LevelPrefab = EditorGUILayout.ObjectField(placeHolderLevel.LevelPrefab, typeof(GameObject), true) as GameObject;
                if(GUILayout.Button("Remove Level"))
                {
                    RemoveLevel(level);
                }
            }
        }
        EditorGUILayout.EndScrollView();

    }

    public void AddLevel()
    {
        //placeHolderEnemy.EnemyId = placeHolderEnemy.GetHashCode().ToString();
        if (placeHolderLevel == null)
        {
            placeHolderLevel = new Level();
            placeHolderLevel.EnemyAmounts = 0;
            placeHolderLevel.EnemiesAlive = 0;
            placeHolderLevel.Done = false;
            placeHolderLevel.NextLevelPosition = Vector3.zero;
        }
        var tmpLevel = placeHolderLevel;
        LevelManager.Instance.CreateLevel(tmpLevel.LevelPrefab, tmpLevel.CurrentLevelPostion);
        levels.Add(tmpLevel);
    }

    public void RemoveLevel(Level level)
    {
        levels.Remove(level);
        OnGUI();
    }

    public void FromJson()
    {
        if (File.Exists(Application.dataPath + "/LevelsData.json"))
        {
            LevelJsonString = File.ReadAllText(Application.dataPath + "/LevelsData.json");
        }
        else
        {
            levels = LevelManager.Instance.GiveLevels();
        }
        if (LevelJsonString == null || LevelJsonString == string.Empty)
        {
            return;
        }
        levels = JsonHelper.FromJsonList<Level>(LevelJsonString);
    }

    public void SerializeToJson()
    {
        if (levels == null || levels.Count < 1) return;
        //Convert to Json
        LevelJsonString = JsonHelper.ToJsonList(levels);
        Debug.Log(LevelJsonString);
        File.WriteAllText(Application.dataPath + "/LevelsData.json", LevelJsonString);
        OnGUI();
    }

    private string FixJson(string value)
    {
        value = "{\"Levels\":" + value + "}";
        return value;
    }
}
