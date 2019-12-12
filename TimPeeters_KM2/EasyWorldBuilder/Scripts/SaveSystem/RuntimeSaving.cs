using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


namespace WorldBuilderTool
{
    public class RuntimeSaving : MonoBehaviour
    {
        public EasyWorldBuilder Manager;
        public Dictionary<Transform, string> SaveableAssets = new Dictionary<Transform, string>(); //Assets to be serialized and saved.
        public List<GameObject> IDList = new List<GameObject>();

        //Undo/Redo Functionality
        public List<GameObject> PlacementHistory = new List<GameObject>();
        public int historyIndex;

        private void Start()
        {
            Manager = EasyWorldBuilder.Instance;
        }

        private void Update()
        {
            for (int i = 0; i < Manager.PlaceableAssets.Count; i++)
            {
                if (!IDList.Contains(Manager.PlaceableAssets[i]) && Manager.PlaceableAssets[i] != null)
                {
                    IDList.Add(Manager.PlaceableAssets[i]);
                }
            }
        }

        public void Undo()
        {
            if (historyIndex > 0)
            {
                SaveableAssets.Remove(SaveableAssets.ElementAt(historyIndex - 1).Key);
                Destroy(PlacementHistory[historyIndex - 1]);
                PlacementHistory.RemoveAt(historyIndex - 1);
                historyIndex--;
            }
        }

        public void AddPrefabForSaving(GameObject go)
        {
            PlacementHistory.Add(go);
            historyIndex++;

            string id = "";
            for (int i = 0; i < IDList.Count; i++)
            {
                if (go.name == IDList[i].name)
                {
                    id = IDList[i].name;
                    SaveableAssets.Add(go.transform, id);
                }
            }
        }

        public void SavePlacedAssets()
        {
            SaveData saveData = new SaveData();
            SaveToFile saveFile = new SaveToFile();

            for (int i = 0; i < SaveableAssets.Count; i++)
            {
                SerializedPrefab savePrefab = new SerializedPrefab();
                savePrefab.Transform = SerializedTransformExtention.SerializeTransform(SaveableAssets.ElementAt(i).Key);
                savePrefab.ID = SaveableAssets.ElementAt(i).Value;

                saveData.SaveList.Add(savePrefab);
            }

            saveFile.WriteData(saveData);
        }

        
    }
}
