using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
public abstract class Ghost : MonoBehaviour, IScore
{
    protected Transform target = null;
    protected Movement2D movement;

    [SerializeField]
    private float scoreValue = 200f;

    public float ScoreValue => scoreValue;

    protected virtual void Awake()
    {
        target = Player.Instance.transform;
        movement = GetComponent<Movement2D>();
    }

    public void Consume()
    {
        ScoreManager.Instance.AddScore(ScoreValue);
    }
}
