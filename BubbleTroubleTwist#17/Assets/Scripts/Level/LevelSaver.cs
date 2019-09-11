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

    public void FromJson()
    {
        if (File.Exists(Application.dataPath + "/EnemyJsonFile.json"))
        {
            LevelJsonString = File.ReadAllText(Application.dataPath + "/EnemyJsonFile.json");
        }
        if (LevelJsonString == null || LevelJsonString == string.Empty)
        {
            return;
        }
        //string enemiesFromJsonString = FixJson(EnemyJsonString);
        LevelData = JsonHelper.FromJsonList<Level>(LevelJsonString);
        Debug.Log(LevelData);
    }

    public void SerializeToJson()
    {
        if (LevelData == null || LevelData.Count < 1) return;
        //Convert to Json
        LevelJsonString = JsonHelper.ToJsonList(LevelData);
        Debug.Log(LevelJsonString);
        File.WriteAllText(Application.dataPath + "/EnemyJsonFile.json", LevelJsonString);
    }

    private string FixJson(string value)
    {
        value = "{\"Enemies\":" + value + "}";
        return value;
    }
}
