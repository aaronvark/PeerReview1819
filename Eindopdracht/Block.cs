using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Color _color;
    public Player owner;
    private Block[] _touching;
    public Rigidbody2D rigidB;

    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
    }

    private void ColorCheck()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(owner != null) { owner.NewBlock(); owner = null; }
    }
}
