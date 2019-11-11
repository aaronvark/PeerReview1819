using UnityEngine;
using UnityEditor;
using System;

namespace NoStrings
{
    [Serializable]
    public class ControllerSettings : SubSettings
    {
        public ControllerSettings(string fileName) : base(fileName)
        {

        }
    }
}