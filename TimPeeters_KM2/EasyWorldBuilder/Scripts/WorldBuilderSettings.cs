using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WorldBuilderTool
{
    //[CreateAssetMenu(fileName = "EasyWorldBuilder_Settings", menuName = "EasyWorldBuilder/Settings", order = 1)]
    
    public class WorldBuilderSettings : ScriptableObject {
        
        public List<GameObject> AssetList = new List<GameObject>();

        #region Keybindings
        public KeyCode PlaceButton = KeyCode.Mouse0;

        public KeyCode RotRight = KeyCode.E;
        public KeyCode RotLeft = KeyCode.Q;

        [Tooltip("This field contains the name of the Input Axis that is used to scale object, " +
        "by default this is set to the 'Mouse ScrollWheel' found in the Unity Input Manager (Legacy)")]
        public string ScaleAxis = "Mouse ScrollWheel";

        #endregion

        #region Control Settings
        public float FlySpeed = 1f;
        public float ScaleSpeed = 1f;
        public float RotationSpeed = 1f;
        #endregion

        #region Placement Options
        public bool PivotSnap;
        public bool SnapGrid;
        public Vector3 GridSize;
        #endregion
    }
}
