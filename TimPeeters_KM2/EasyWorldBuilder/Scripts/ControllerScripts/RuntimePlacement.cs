using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace WorldBuilderTool
{
    public enum PlacementMode
    {
        Flying,
        FirstPerson
    }

    public class RuntimePlacement : MonoBehaviour
    {
        [HideInInspector] public PlacementMode CurrentMode;
        [HideInInspector] public Camera FlyCam;
        [HideInInspector] public Camera FpsCam;
        public Camera CurrentCam;
        public bool IsRotating;

        EasyWorldBuilder _manager;
        RuntimeSaving _saveSys;
        SimpleCameraController flyControl;
        FirstPersonController fpsControl;
        CharacterController _charControl;
        GameObject _currentAsset; //The object to be placed
        GameObject _placementParent; //The parent object of the current asset to be placed.
        RaycastHit _hit;


        private void Awake()
        {
            _manager = FindObjectOfType<EasyWorldBuilder>();
            _saveSys = FindObjectOfType<RuntimeSaving>();

            FlyCam = gameObject.AddComponent<Camera>();
            _charControl = gameObject.AddComponent<CharacterController>();
            gameObject.AddComponent<AudioListener>();

            fpsControl = gameObject.AddComponent<FirstPersonController>();
            FpsCam = fpsControl.m_Camera;

            flyControl = gameObject.AddComponent<SimpleCameraController>();
        }

        private void Update()
        {
            switch (CurrentMode)
            {
                case PlacementMode.Flying:
                    CurrentCam = FlyCam;
                    fpsControl.m_Camera.gameObject.SetActive(false);
                    FlyCam.enabled = true;
                    fpsControl.enabled = false;
                    flyControl.enabled = true;
                    break;
                case PlacementMode.FirstPerson:
                    fpsControl.m_Camera.gameObject.SetActive(true);
                    CurrentCam = FpsCam;
                    FlyCam.enabled = false;
                    flyControl.enabled = false;
                    fpsControl.enabled = true;
                    break;

            }

            if (_manager.SelectedAsset != null)
            {
                DeterminePlacementLocation();
            }
        }

        void DeterminePlacementLocation()
        {
            Ray ray = CurrentCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);

            if (Physics.Raycast(ray, out _hit))
            {
                if (_placementParent == null)
                {
                    _placementParent = new GameObject();
                    _placementParent.transform.position = _hit.point;
                    _placementParent.name = "Asset Placer";
                    //placementParent.hideFlags = HideFlags.HideInHierarchy;
                }

                if (_currentAsset == null)
                {
                    _currentAsset = Instantiate(_manager.SelectedAsset, _placementParent.transform);
                    //currentAsset.transform.parent = placementParent.transform;
                    foreach (Collider col in _currentAsset.GetComponentsInChildren<Collider>())
                    {
                        col.enabled = false;
                    }
                    _currentAsset.transform.localPosition = Vector3.zero;
                }

                Debug.DrawRay(_currentAsset.transform.position, _currentAsset.transform.up * 2f, Color.magenta);
                _placementParent.transform.up = _hit.normal;
                _placementParent.transform.position = _hit.point;
            }
        }

        public void RotateRight()
        {
            if (_currentAsset != null)
            {
                _currentAsset.transform.localEulerAngles += new Vector3(0, _manager.Settings.RotationSpeed, 0);
            }
        }

        public void RotateLeft()
        {
            if (_currentAsset != null)
            {
                _currentAsset.transform.localEulerAngles -= new Vector3(0, _manager.Settings.RotationSpeed, 0);
            }
        }

        public void DoScale(float axis)
        {
            _currentAsset.transform.localScale += new Vector3(axis, axis, axis);

            Vector3 newScale = new Vector3();
            newScale.x = Mathf.Clamp(_currentAsset.transform.localScale.x, 0, Mathf.Infinity);
            newScale.y = Mathf.Clamp(_currentAsset.transform.localScale.y, 0, Mathf.Infinity);
            newScale.z = Mathf.Clamp(_currentAsset.transform.localScale.z, 0, Mathf.Infinity);
            _currentAsset.transform.localScale = newScale;
        }

        public void PlaceAsset()
        {
            Assert.IsNotNull(_currentAsset);
            _saveSys.AddPrefabForSaving(_currentAsset); //Prepare prefab for saving
            _placementParent.transform.DetachChildren();
            _currentAsset.transform.position = _hit.point;
            foreach (Collider col in _currentAsset.GetComponentsInChildren<Collider>())
            {
                col.enabled = true;
            }
            _currentAsset = null;
        }

    }
}
