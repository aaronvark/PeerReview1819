using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldBuilderTool
{
    public enum PlacementMode
    {
        Flying,
        FirstPerson
    }

    public class RuntimePlacement : MonoBehaviour
    {
        EasyWorldBuilder mgr;

        [HideInInspector] public PlacementMode curMode;

        SimpleCameraController flyControl;
        FirstPersonController fpsControl;

        [HideInInspector] public Camera _cam;
        [HideInInspector] public Camera _fpsCam;
        public Camera currentCam;

        CharacterController _charControl;

        [SerializeField] GameObject currentAsset;
        GameObject placementParent; //The parent object of the current asset that handles normal snapping

        RaycastHit hit;

        public bool isRotating;

        private void Awake()
        {
            mgr = FindObjectOfType<EasyWorldBuilder>();

            _cam = gameObject.AddComponent<Camera>();
            _charControl = gameObject.AddComponent<CharacterController>();
            gameObject.AddComponent<AudioListener>();

            fpsControl = gameObject.AddComponent<FirstPersonController>();
            _fpsCam = fpsControl.m_Camera;

            flyControl = gameObject.AddComponent<SimpleCameraController>();
        }

        private void Update()
        {
            switch (curMode)
            {
                case PlacementMode.Flying:
                    currentCam = _cam;
                    fpsControl.m_Camera.gameObject.SetActive(false);
                    _cam.enabled = true;


                    fpsControl.enabled = false;
                    flyControl.enabled = true;

                    break;
                case PlacementMode.FirstPerson:
                    fpsControl.m_Camera.gameObject.SetActive(true);
                    currentCam = _fpsCam;
                    _cam.enabled = false;


                    flyControl.enabled = false;
                    fpsControl.enabled = true;
                    break;

            }

            if (mgr.selectedAsset != null)
            {
                DeterminePlacementLocation();
            }
        }

        void DeterminePlacementLocation()
        {
            Ray ray = currentCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);


            if (Physics.Raycast(ray, out hit))
            {
                if (placementParent == null)
                {
                    placementParent = new GameObject();
                    placementParent.transform.position = hit.point;
                    placementParent.name = "Asset Placer";
                    //placementParent.hideFlags = HideFlags.HideInHierarchy;
                }

                if (currentAsset == null)
                {
                    currentAsset = Instantiate(mgr.selectedAsset, placementParent.transform);
                    //currentAsset.transform.parent = placementParent.transform;
                    foreach (Collider col in currentAsset.GetComponentsInChildren<Collider>())
                    {
                        col.enabled = false;
                    }

                    currentAsset.transform.localPosition = Vector3.zero;

                }

                Debug.DrawRay(currentAsset.transform.position, currentAsset.transform.up * 2f, Color.magenta);

                placementParent.transform.up = hit.normal;
                placementParent.transform.position = hit.point;

            }
        }

        public void RotateRight()
        {
            currentAsset.transform.localEulerAngles += new Vector3(0, mgr.settings.rotationSpeed, 0);
        }

        public void RotateLeft()
        {
            currentAsset.transform.localEulerAngles -= new Vector3(0, mgr.settings.rotationSpeed, 0);
        }

        public void DoScale(float axis)
        {
            currentAsset.transform.localScale += new Vector3(axis, axis, axis);

            Vector3 newScale = new Vector3();
            newScale.x = Mathf.Clamp(currentAsset.transform.localScale.x, 0, Mathf.Infinity);
            newScale.y = Mathf.Clamp(currentAsset.transform.localScale.y, 0, Mathf.Infinity);
            newScale.z = Mathf.Clamp(currentAsset.transform.localScale.z, 0, Mathf.Infinity);
            currentAsset.transform.localScale = newScale;
        }

        public void PlaceAsset()
        {
            mgr.PlacedAssets.Add(currentAsset);

            placementParent.transform.DetachChildren();

            currentAsset.transform.position = hit.point;

            foreach (Collider col in currentAsset.GetComponentsInChildren<Collider>())
            {
                col.enabled = true;
            }

            currentAsset = null;
        }

    }
}
