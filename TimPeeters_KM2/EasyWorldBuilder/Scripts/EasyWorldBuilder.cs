using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

namespace WorldBuilderTool
{
    [RequireComponent(typeof(RuntimeSaving))]
    public class EasyWorldBuilder : MonoBehaviour
    {
        #region Singleton
        private static EasyWorldBuilder _instance;

        public static EasyWorldBuilder Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        public RuntimePlacement Controller;
        public List<GameObject> PlaceableAssets = new List<GameObject>(); //List of assets that can be placed in the scene
        public GameObject SelectedAsset;
        public RuntimeUI ControllerUI;
        public RuntimeSaving SaveSys;
        public WorldBuilderSettings Settings;

        private bool onClick;


        private void OnDestroy()
        {
            Destroy(Controller.gameObject);
        }

        public void SpawnController(PlacementMode mode)
        {
            PlaceableAssets = Settings.AssetList;
            SaveSys = GetComponent<RuntimeSaving>();

            Controller = new GameObject().AddComponent<RuntimePlacement>();
            Controller.name = "WorldBuilder Controller";
            Controller.transform.position = SceneView.lastActiveSceneView.camera.transform.position;

            ControllerUI = Instantiate(Resources.Load<GameObject>("EasyWorldBuilder/WorldBuilder_RuntimeUI")).GetComponent<RuntimeUI>();

            if (mode == PlacementMode.Flying)
            {
                Controller.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
            }
        }


        private void Update()
        {
            //Assets can change at runtime.
            PlaceableAssets = Settings.AssetList;

            if (SelectedAsset != null && !EventSystem.current.IsPointerOverGameObject())
            {
                //Input handling
                if (Input.GetKeyDown(Settings.PlaceButton) && !EventSystem.current.IsPointerOverGameObject())
                {
                    Controller.PlaceAsset();
                }

                if (Input.GetKey(Settings.RotLeft))
                {
                    Controller.RotateLeft();
                    Controller.IsRotating = true;
                }
                else
                {
                    Controller.IsRotating = false;
                }

                if (Input.GetKey(Settings.RotRight))
                {
                    Controller.RotateRight();
                    Controller.IsRotating = true;
                }
                else
                {
                    Controller.IsRotating = false;
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    Controller.DoScale(Input.GetAxis("Mouse ScrollWheel") * Settings.ScaleSpeed);
                }


                //Undo function
                if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Z))
                {
                    SaveSys.Undo();
                }


                //TODO redo function
                if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) 
                    && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                    && Input.GetKeyDown(KeyCode.Z))
                {
                    //SaveSys.Redo();
                }
            }
        }
    }
}
