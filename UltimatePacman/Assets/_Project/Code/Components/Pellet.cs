using UnityEngine;

public class Pellet : MonoBehaviour, IScore, ISound
{
    public static int PelletCount { get; private set; } = 0;    // Keep a reference to the number of pellets

    [SerializeField]
    protected int scoreValue = 10;
    [SerializeField]
    protected AudioClip consumedSound = null;

    public float ScoreValue => scoreValue;
    public SoundClip[] SoundClips => new[] { new SoundClip(consumedSound) };

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
        SoundManager.Instance.PlaySoundClipAtPoint(this);

        if(Toolbox.Instance.TryGetValue<GameManager>(out GameManager gameManager))
        {
            gameManager.CheckPellets();
        }
    }
}
