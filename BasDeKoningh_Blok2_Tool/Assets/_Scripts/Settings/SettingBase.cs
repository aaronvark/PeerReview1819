using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class SettingBase
    {
        public bool Show = false;
        public void ShowBase()
        {
            Show = true;
        }

        public void CloseBase()
        {
            Show = false;
        }
    }
}
