using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{

    [SerializeField]
    private Rigidbody myRb;

    [Range(0, 1)]
    [SerializeField]
    private float rotateMultiplier;
    [Range(0, 1)]
    [SerializeField]
    private float movementMultiplier;
    [Range(0, 10)]
    [SerializeField]
    private float gravityModifier;

    public delegate void BlockCallBack(Block b);
    public BlockCallBack _onHitGround;

    public float Hitpoints
    {
        get;
        set;
    }

    [SerializeField]
    private Renderer renderer;

    [SerializeField]
    private Material[] materials;

    protected void Start()
    {
        InputManager.Instance.horizontalMovement += Move;
        InputManager.Instance.rotateMovement += Rotate;
    }

    protected void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    protected void Update()
    {
        Fall();
    }
    public void GetDamage(float _damage)
    {
        Hitpoints -= _damage;
        if (Hitpoints < 0)
            Pool.Instance.ReturnObjectToPool<Block>(this);
    }


    private void Fall()
    {
        myRb.AddForce(Vector3.down * gravityModifier);
    }

    /// <summary>
    /// Rotating the Tetris block
    /// </summary>
    /// <param name="rotateMultiplier"></param>
    private void Rotate(float _rotate)
    {
        myRb.AddTorque(new Vector3(myRb.rotation.x, myRb.rotation.y, myRb.rotation.z + (_rotate * rotateMultiplier)));
    }

    /// <summary>
    /// Moving the Tetris block
    /// </summary>
    /// <param name="moveMultiplier"></param>
    public void Move(float _movement)
    {
        Vector3 _vector = new Vector3(_movement * movementMultiplier, 0, 0);
        myRb.MovePosition(transform.position + _vector);
    }

    /// <summary>
    /// When the block hit the ground or a block it will update Delegates 
    /// </summary>
    public virtual void OnCollisionEnter(Collision _coll)
    {
        Block _block = _coll.gameObject.GetComponent<Block>();
        if (_coll.gameObject.layer == LayerMask.NameToLayer("Ground") || _block != null)
        {
            _onHitGround?.Invoke(this);

            InputManager.Instance.horizontalMovement -= Move;
            InputManager.Instance.rotateMovement -= Rotate;
        }
    }
}
