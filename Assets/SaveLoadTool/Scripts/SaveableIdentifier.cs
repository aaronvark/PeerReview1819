using System.Collections.Generic;
using UnityEngine;

namespace Common.SaveLoadSystem
{
    public class SaveableIdentifier : MonoBehaviour, Iidentifier
    {
        public int _id;
        public int id 
        {
            get { return _id; }
            set 
            { 
                _id = (_id != 0) ? _id : value; 
            }
        }

        public List<ComponentSave> _componentSave;
        public List<ComponentSave> componentSaves 
        { 
            get { return _componentSave; } 
            set { _componentSave = value; }
        }
    }
}
