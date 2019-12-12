using System.Collections.Generic;
using UnityEngine;

namespace Common.SaveLoadSystem
{
    [System.Serializable]
    public struct ComponentSave
    {
        public string componentTypeName;
        public bool boolItem;
        public List<bool> boolList;

        public ComponentSave(string typeName, bool boolItem, List<bool> boolList)
        {
            componentTypeName = typeName;
            this.boolItem = boolItem;
            this.boolList = boolList;
        }
    }
}
