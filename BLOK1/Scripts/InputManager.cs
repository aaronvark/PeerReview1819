using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    /// <summary>
    /// Horizontal movement of the blocks
    /// </summary>
    /// <param name="_movement"></param>
    public delegate void HorizontalMovement(float _movement);
    public event HorizontalMovement horizontalMovement;

    /// <summary>
    /// Rotating movement of the block
    /// </summary>
    /// <param name="_rotate"></param>
    public delegate void RotateMovement(float _rotate);
    public event RotateMovement rotateMovement;

    /// <summary>
    /// Fixed update is used for the 
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
            horizontalMovement?.Invoke(Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.X))
            rotateMovement?.Invoke(1);
        if (Input.GetKey(KeyCode.Z))
            rotateMovement?.Invoke(-1);
    }
}
