using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Color _color;
    public Player owner;
    //public List<Block> _touching; //temp public
    public HashSet<Block> connected = new HashSet<Block>();
    public Rigidbody2D rigidB;
    private LineRenderer lineR;

    private float _firstTouch = Mathf.Infinity;

    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        lineR = GetComponentInChildren<LineRenderer>();
        lineR.startColor = owner.playerColor;
        lineR.endColor = lineR.startColor;
    }


    void Update()
    {
        //if (Time.time > _firstTouch + 1f)
        //{
        //    lineR.startColor = new Color(0, 0, 0, 0);
        //    lineR.endColor = lineR.startColor;
        //}
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < _firstTouch) { owner.Invoke("NewBlock", 1f); _firstTouch = Time.time; Debug.Log("Bump"); }
        Block _otherBlock = other.transform.parent.GetComponent<Block>();
        if (_otherBlock != null) {
            
            //Adding connected blocks of same color to hashset
            if (_otherBlock._color == _color)
            {
                connected.Add(_otherBlock);
                foreach (Block b in _otherBlock.connected) {
                    connected.Add(b);
                }
            }

            //If 3 blocks are connected shortly after getting placed, remove them and award points

            if ( connected.Count >= 3)
            {
                    owner.AddScore(30);
                    foreach (Block b in connected)
                    {
                        GameManager.Instance.blocks.Remove(b);
                        Destroy(b.gameObject, Time.deltaTime);
                    }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Block _otherBlock = other.transform.parent.GetComponent<Block>();
        if (_otherBlock != null) { connected.Remove(_otherBlock); }
    }
}
