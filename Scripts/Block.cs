using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{

    [SerializeField]
    private Rigidbody _myRb;

    [Range(0, 1)]
    [SerializeField]
    private float _rotateMultiplier;
    [Range(0, 1)]
    [SerializeField]
    private float _movementMultiplier;
    [Range(0, 10)]
    [SerializeField]
    private float _gravityModifier;

    public delegate void BlockCallBack(Block b);
    public BlockCallBack _onHitGround;

    public float Hitpoints
    {
        get;
        set;
    }

    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    private Material[] _materials;

    public void Start()
    {
        InputManager.Instance._horizontalMovement += Move;
        InputManager.Instance._rotateMovement += Rotate;
    }

    public void Update()
    {
        Fall();
    }
    public void GetDamage(float damage)
    {
        Hitpoints -= damage;
    }


    public void Fall()
    {
        _myRb.AddForce(Vector3.down * _gravityModifier);
    }

    /// <summary>
    /// Rotating the Tetris block
    /// </summary>
    /// <param name="rotateMultiplier"></param>
    public void Rotate(float rotate)
    {
        _myRb.AddTorque(new Vector3(_myRb.rotation.x, _myRb.rotation.y, _myRb.rotation.z + (rotate * _rotateMultiplier)));
    }

    /// <summary>
    /// Moving the Tetris block
    /// </summary>
    /// <param name="moveMultiplier"></param>
    public void Move(float movement)
    {
        Vector3 vector = new Vector3(movement * _movementMultiplier, 0, 0);
        _myRb.MovePosition(transform.position + vector);
    }

    /// <summary>
    /// When the block hit the ground or a block it will update Delegates 
    /// </summary>
    /// <param name="coll"></param>
    public void OnCollisionEnter(Collision coll)
    {
        Block block = coll.gameObject.GetComponent<Block>();
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground") || block != null)
        {
            _onHitGround?.Invoke(this);
            InputManager.Instance._horizontalMovement -= Move;
            InputManager.Instance._rotateMovement -= Rotate;
        }
    }
}
