using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    /// <summary>
    /// Horizontal movement of the blocks
    /// </summary>
    /// <param name="movement"></param>
    public delegate void HorizontalMovement(float movement);
    public event HorizontalMovement _horizontalMovement;

    /// <summary>
    /// Rotating movement of the block
    /// </summary>
    /// <param name="rotate"></param>
    public delegate void RotateMovement(float rotate);
    public event RotateMovement _rotateMovement;

    /// <summary>
    /// Fixed update is used for the 
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
            _horizontalMovement?.Invoke(Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.X))
            _rotateMovement?.Invoke(1);
        if (Input.GetKey(KeyCode.Z))
            _rotateMovement?.Invoke(-1);
    }
}
