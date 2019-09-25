using Breakin.GameManagement;
using UnityEngine;

namespace Breakin.Behaviour
{
    public class BatMovement : MonoBehaviour
    {
        [SerializeField] private float radius;

        private Camera cam;

        public float Angle { get; private set; }
        public float Radius => radius;

        private void Start()
        {
            // Load camera
            cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

            EventManager.gameUpdate += OnUpdate;
            EventManager.levelSetup += OnReset;
        }

        private void OnDestroy()
        {
            EventManager.gameUpdate -= OnUpdate;
            EventManager.levelSetup -= OnReset;
        }

        private void OnDrawGizmos()
        {
            // Handy gizmo that visualizes the sphere where the bat can go
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Vector3.zero, radius);
        }

        private void OnUpdate()
        {
            UpdateAngleFromMousePos();

            ApplyRotation();
            ApplyPosition();
        }

        private void OnReset()
        {
            Angle = 0;

            ApplyPosition();
            ApplyRotation();
        }

        /// <summary>
        /// Calculate the angle from the center of the ring towards the mouse cursor
        /// </summary>
        private void UpdateAngleFromMousePos()
        {
            // Get the mouse pos in pixels
            Vector3 _mousePos = Input.mousePosition;
            // Turn the pixels into a point in the game world; use Vector2 to discard z-coordinate (not used in 2d)
            Vector2 _mousePosWorld = cam.ScreenToWorldPoint(_mousePos);

            // The angle of the bat should be relative to the positive x-axis (same as on a unit circle)
            Angle = Vector2.SignedAngle(Vector2.right, _mousePosWorld.normalized);
        }

        private void ApplyRotation()
        {
            // Set angle as the z-component of the rotation
            transform.localRotation = Quaternion.Euler(0, 0, Angle);
        }

        private void ApplyPosition()
        {
            // Convert angle to radians
            float _rad = Angle * Mathf.Deg2Rad;
            // Calculate the position based on the current angle, where x = cos(a) * r ^ y = sin(a) * r
            Vector3 _newPos = new Vector3(Mathf.Cos(_rad), Mathf.Sin(_rad)) * radius;

            // Set the position
            transform.position = _newPos;
        }
    }
}