using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 90f;

    private Rigidbody2D rigidbody2D;

    private Vector2 CurrentPosition { get => rigidbody2D.position; }
    private float CurrentRotation { get => rigidbody2D.rotation; }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 positionChange = direction * moveSpeed * Time.deltaTime;
        Vector2 newPosition = CurrentPosition + positionChange;

        rigidbody2D.MovePosition(newPosition);
    }

    public void Rotate(float torque)
    {
        float rotationChange = torque * rotationSpeed * Time.deltaTime;
        float newRotation = CurrentRotation + rotationChange;

        rigidbody2D.MoveRotation(newRotation);
    }
}
