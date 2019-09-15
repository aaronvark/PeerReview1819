using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int type = 0; //types from 0-5
    public Player owner;
    public HashSet<Block> connected = new HashSet<Block>();
    private Rigidbody2D rigidB;
    public Rigidbody2D GetRigidB() { return rigidB; }

    private float _firstTouch = Mathf.Infinity;

    void Start()
    {
        if (rigidB == null) { rigidB = GetComponent<Rigidbody2D>(); }
        Randomize();
    }



    public void Randomize()
    {
        transform.GetChild(type).gameObject.SetActive(false);
        type = Random.Range(0,6);
        transform.GetChild(type).gameObject.SetActive(true);
    }

    public void Move(float _x, float _y)
    {
        rigidB.velocity = new Vector2(_x, _y) * GameManager.Instance.blockSpeed;
    }

    public void Rotate(float _direction)
    {
        transform.Rotate(new Vector3(0, 0, _direction * -1f));
    }

    public void Attract(Vector3 target, float force)
    {
        rigidB.AddForce(target - transform.position.normalized * force);
    }

    //TODO: Fix multiple collisions awarding points 
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (Time.time < _firstTouch)
        {
            owner.Invoke("NewBlock", 1f); //TODO: change delay without invoke, because delay needs to be instant if it connects a pair
            _firstTouch = Time.time;
            GameManager.Instance.AddBlock(this);
            GameManager.Instance.Attract += Attract;
        }
        Block _otherBlock = _other.transform.parent?.parent?.GetComponent<Block>();
        if (_otherBlock != null)
        {

            //Adding connected blocks of same type to hashset
            if (_otherBlock.type == type)
            {
                connected.Add(_otherBlock);
                connected.UnionWith(_otherBlock.connected);
                _otherBlock.connected.UnionWith(connected);
            }

            //If 3 blocks are connected remove them and award points
            if (connected.Count >= 3)
            {

                if (transform.position.x < 0)
                {
                    GameManager.Instance.players[0].AddScore(30);
                }
                else
                {
                    GameManager.Instance.players[1].AddScore(30);
                }

                connected.Remove(this);
                foreach (Block b in new HashSet<Block>(connected))
                {
                    GameManager.Instance.RemoveBlock(b);
                    GameManager.Instance.blockPool.Return(b.gameObject);
                }
                GameManager.Instance.blockPool.Return(this.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        Block _otherBlock = _other.transform.parent?.parent?.GetComponent<Block>();
        if (_otherBlock != null) { connected.Remove(_otherBlock); }
    }
}