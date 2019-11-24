using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace WorldBuilderTool
{
    public class EasyWorldBuilder : MonoBehaviour
    {
        public RuntimePlacement controller;

        public List<GameObject> PlaceableAssets = new List<GameObject>(); //List of assets that can be placed in the scene

        public List<GameObject> PlacedAssets = new List<GameObject>(); //List of assets that have been placed at runtime.

        public GameObject selectedAsset;

        public RuntimeUI controllerUI;

        public WorldBuilderSettings settings;

        private void OnDestroy()
        {
            Destroy(controller.gameObject);
        }

        public void SpawnController(PlacementMode mode)
        {

            controller = new GameObject().AddComponent<RuntimePlacement>();

            controller.name = "WorldBuilder Controller";

            controller.transform.position = SceneView.lastActiveSceneView.camera.transform.position;

            controllerUI = Instantiate(Resources.Load<GameObject>("EasyWorldBuilder/WorldBuilder_RuntimeUI")).GetComponent<RuntimeUI>();

            if (mode == PlacementMode.Flying)
            {
                controller.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
            }

            PlaceableAssets = settings.AssetList;

        }

        private void Update()
        {
            //Assets can change at runtime.
            PlaceableAssets = settings.AssetList;

            if (selectedAsset != null)
            {
                //Input handling
                if (Input.GetKeyDown(settings.placeButton))
                {
                    controller.PlaceAsset();
                }

                if (Input.GetKey(settings.rotLeft))
                {
                    controller.RotateLeft();
                    controller.isRotating = true;
                }
                else
                {
                    controller.isRotating = false;
                }

                if (Input.GetKey(settings.rotRight))
                {
                    controller.RotateRight();
                    controller.isRotating = true;
                }
                else
                {
                    controller.isRotating = false;
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    controller.DoScale(Input.GetAxis("Mouse ScrollWheel") * settings.scaleSpeed);
                }
            }
        }
    }
}
