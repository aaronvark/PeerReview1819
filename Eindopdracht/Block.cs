using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Color _color;
    public Player owner;
    public HashSet<Block> connected = new HashSet<Block>();
    private Rigidbody2D rigidB;
    public Rigidbody2D GetRigidB() { return rigidB; }
    private LineRenderer lineR;

    private float _firstTouch = Mathf.Infinity;

    void Start()
    {
        //TODO: Set sprite color based on block color
        rigidB = GetComponent<Rigidbody2D>();
        lineR = GetComponentInChildren<LineRenderer>();
        lineR.startColor = owner.playerColor;
        lineR.endColor = lineR.startColor;
    }


    void Update()
    {
        //disable lineRenderer after 1 sec
        //if (Time.time > _firstTouch + 1f)
        //{
        //    lineR.startColor = new Color(0, 0, 0, 0);
        //    lineR.endColor = lineR.startColor;
        //}
    }

    public void Move(float _x, float _y)
    {
        rigidB.velocity = new Vector2(_x, _y) * GameManager.Instance.blockSpeed;        
    }

    public void Rotate(float _direction)
    {
        transform.Rotate(new Vector3(0, 0, _direction * -1f));        
    }

    //TODO: when block hits trigger event?
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (Time.time < _firstTouch) {
            owner.Invoke("NewBlock", 1f);
            _firstTouch = Time.time;
            GameManager.Instance.AddBlock(this);
        }
        Block _otherBlock = _other.transform.parent.GetComponent<Block>();
        if (_otherBlock != null) {
            
            //Adding connected blocks of same color to hashset
            if (_otherBlock._color == _color)
            {
                connected.Add(_otherBlock);
                foreach (Block b in _otherBlock.connected) {
                    connected.Add(b);
                }
            }

            //If 3 blocks are connected remove them and award points
            if ( connected.Count >= 3)
            {
                    owner.AddScore(30);
                    foreach (Block b in connected)
                    {
                        GameManager.Instance.RemoveBlock(b);
                        Destroy(b.gameObject, Time.deltaTime);
                    }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        Block _otherBlock = _other.transform.parent.GetComponent<Block>();
        if (_otherBlock != null) { connected.Remove(_otherBlock); }
    }
}
