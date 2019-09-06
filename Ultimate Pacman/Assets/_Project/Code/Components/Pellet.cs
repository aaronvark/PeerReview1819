using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour, IScore
{
    public static int PelletCount { get; private set; } = 0;

    [SerializeField]
    protected int scoreValue = 10;

    public float ScoreValue => scoreValue;

    protected virtual void OnEnable()
    {
        PelletCount++;
    }

    protected virtual void OnDisable()
    {
        PelletCount--;
    }

    public virtual void Consume()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ScoreValue);
        GameManager.Instance.CheckPellets();
    }
}
