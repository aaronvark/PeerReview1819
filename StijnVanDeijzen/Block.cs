using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int type = 0; //types from 0-5
    public Player owner;
    public HashSet<Block> connected = new HashSet<Block>();
    private Rigidbody2D rigidB;

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

    public void SetFreeze(bool value)
    {
        if (rigidB != null)
        {
            rigidB.simulated = !value;
        }
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

    //TODO: Fix multiple collisions awarding points, because multiple blocks trigger at the same time.
    private void OnTriggerEnter2D(Collider2D _other)
    {
        

        if (Time.time < _firstTouch)
        {
            owner.NewBlock(); //TODO: sometimes not called?
            _firstTouch = Time.time;
            GameManager.Instance.Attract += Attract;
        }
        Block _otherBlock = _other.transform.parent?.parent?.GetComponent<Block>();
        if (_otherBlock != null)
        {

            //Adding connected blocks of same type to hashset
            //DISCUSS: Does this leave gaps? is this efficient?
            if (_otherBlock.type == type)
            {
                connected.UnionWith(_otherBlock.connected);
                connected.Add(_otherBlock);
                _otherBlock.connected.UnionWith(connected);
            }

            //If 3 blocks are connected remove them and award points
            if (connected.Count >= 3)
            {
                Debug.Log("Instance " + gameObject.GetInstanceID() +" hit " + _other.GetInstanceID() + " - Time: " + Time.time);
                if (gameObject.GetInstanceID() > _other.GetInstanceID())//only 1 object will execute, hopefully
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
                        BlockPool.Return(b.gameObject);
                    }
                    BlockPool.Return(this.gameObject); 
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {
        Block _otherBlock = _other.transform.parent?.parent?.GetComponent<Block>();
        if (_otherBlock != null) { connected.Remove(_otherBlock); }
    }
}