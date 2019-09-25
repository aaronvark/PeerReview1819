using UnityEngine;

namespace Breakin.Behaviour
{
    [RequireComponent(typeof(BatMovement))]
    public class Bat : MonoBehaviour
    {
        private BatMovement movement;

        /// <summary>
        /// Direction of the flat side of the bat pointing outward
        /// </summary>
        public Vector3 Dir
        {
            get
            {
                float _angleRad = movement.Angle * Mathf.Deg2Rad;
                return new Vector2(Mathf.Cos(_angleRad), Mathf.Sin(_angleRad)).normalized;
            }
        }

        /// <summary>
        /// Radius of the circle on which the bat can move
        /// </summary>
        public float Radius => movement.Radius;

        private void Start()
        {
            // Load the reference to the movement class
            movement = GetComponent<BatMovement>();
        }
    }
}