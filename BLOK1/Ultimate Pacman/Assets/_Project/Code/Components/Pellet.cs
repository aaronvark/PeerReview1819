using UnityEngine;

public class Pellet : MonoBehaviour, IScore
{
    public static int PelletCount { get; private set; } = 0;    // Keep a reference to the number of pellets

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
        ScoreManager.Instance.AddScore(this);
        GameManager.Instance.CheckPellets();
    }
}
