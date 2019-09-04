using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField]
    private Rigidbody myRb;

    [Range(1,1000)]
    [SerializeField]
    private float _rotateMultiplier;
    [Range(0,1)]
    [SerializeField]
    private float _movementMultiplier;
    [Range(0,10)]
    [SerializeField]
    private float _gravityModifier;
    private bool _isCurrentBlock;

    public void Start() {
        _isCurrentBlock = true;
    }

    public void Update() {
        Fall();
    }

    public void Fall() {
        myRb.AddForce(Vector3.down * _gravityModifier);
    }

    /// <summary>
    /// Rotating the Tetris block
    /// </summary>
    /// <param name="rotateMultiplier"></param>
    public void Rotate(float rotate) {
        myRb.AddTorque(new Vector3(myRb.rotation.x, myRb.rotation.y, myRb.rotation.z + (rotate * _rotateMultiplier)));
    }

    /// <summary>
    /// Moving the Tetris block
    /// </summary>
    /// <param name="moveMultiplier"></param>
    public void Move(float movement) {
        Vector3 vector = new Vector3(movement * _movementMultiplier, 0, 0);
        myRb.MovePosition(transform.position + vector);
    }

    public void OnCollisionEnter(Collision coll) {
        if(coll.gameObject.layer == LayerMask.NameToLayer("Ground") || 
            coll.gameObject.tag == "Block") {
            if (_isCurrentBlock) {
                InputManager.Instance.UpdateCurrentBlock();
                _isCurrentBlock = false;
            }

        }
    }
}
