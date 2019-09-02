using Shapes2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private float animationSpeedMultiplier = 2f;

    private Rigidbody2D rigidbody;
    private Shape shape;
    private int horizontalInput = 0;
    private float mouthMaxAngle = 60f;
    private float mouthAngleT = 0;
    private float mouthAngleDirection = 1;

    private const float MOUTH_CLOSED_ANGLE = 0;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        shape = GetComponent<Shape>();
    }

    private void Update()
    {
        // Stores the input for use in the fixed update for correct physics handling
        horizontalInput = Mathf.RoundToInt(Input.GetAxis("Horizontal"));

        AnimateMouth();
    }

    private void FixedUpdate() {
        float _deltaTime = Time.deltaTime;

        // Rotates the Player
        float _currentRotation = rigidbody.rotation;
        float _rotationChange = horizontalInput * rotationSpeed * _deltaTime;
        rigidbody.MoveRotation(_currentRotation + _rotationChange);

        // Moves the Player
        Vector2 _currentPosition = rigidbody.position;
        Vector2 _positionChange = (Vector2)transform.right * moveSpeed * _deltaTime;
        rigidbody.MovePosition(_currentPosition + _positionChange);
    }

    private void AnimateMouth() {
        mouthAngleT = Mathf.Clamp01(mouthAngleT + Time.deltaTime * mouthAngleDirection * moveSpeed * animationSpeedMultiplier);

        if (mouthAngleT == 1 || mouthAngleT == 0) {
            mouthAngleDirection *= -1;
        }

        float _mouthAngle = Mathf.Lerp(MOUTH_CLOSED_ANGLE, mouthMaxAngle, mouthAngleT);

        shape.settings.startAngle = _mouthAngle;
        shape.settings.endAngle = 360f - _mouthAngle;
    }
}
