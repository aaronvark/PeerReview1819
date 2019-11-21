using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotationSpeed = 90f;

    private new Rigidbody2D rigidbody2D;

    private Vector2 CurrentPosition => rigidbody2D.position;
    private float CurrentRotation => rigidbody2D.rotation;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves the rigidbody in the specified direction. Use <c>moveSpeed</c> for movement speed.
    /// </summary>
    /// <param name="direction">The direction is normalized within this function.</param>
    public void Move(Vector2 direction)
    {
        direction.Normalize();
        Vector2 positionChange = direction * moveSpeed * Time.deltaTime;
        Vector2 newPosition = CurrentPosition + positionChange;

        rigidbody2D.MovePosition(newPosition);
    }

    /// <summary>
    /// Rotates the rigidbody in the specified rotation. Use <c>rotationSpeed</c> for rotational speed.
    /// </summary>
    /// <param name="torque">The torque is signed to 1, 0 or -1 within this function.</param>
    public void Rotate(float torque)
    {
        int sign = Math.Sign(torque);

        float rotationChange = sign * rotationSpeed * Time.deltaTime;
        float newRotation = CurrentRotation + rotationChange;

        rigidbody2D.MoveRotation(newRotation);
    }
}
