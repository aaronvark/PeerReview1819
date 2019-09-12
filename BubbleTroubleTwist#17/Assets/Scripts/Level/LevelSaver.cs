using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class LevelSaver : MonoBehaviour
{
    public string LevelJsonString;
    public List<Level> LevelData;

    private void Start()
    {
        LevelData = LevelManager.Instance.GiveLevels();
        EventManager.OnSaveLevelHandler += SerializeToJson;
    }

    public void SerializeToJson()
    {
        if (LevelData == null || LevelData.Count < 1) return;
        //Convert to Json
        LevelJsonString = JsonHelper.ToJsonList(LevelData);
        Debug.Log(LevelJsonString);
        File.WriteAllText(Application.dataPath + "/LevelsData.json", LevelJsonString);
    }

    private string FixJson(string value)
    {
        value = "{\"Levels\":" + value + "}";
        return value;
    }
}
