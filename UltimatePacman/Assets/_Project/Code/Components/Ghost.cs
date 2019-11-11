using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ghost : MonoBehaviour, IScore, ISound
{
    [SerializeField]
    private GhostBehaviour behaviour = GhostBehaviour.Chase;
    [SerializeField]
    private Transform flankBase = null;
    [SerializeField]
    private AudioClip consumedClip = null;
    [SerializeField]
    private float scoreValue = 200f;

    public Animator Animator { get; protected set; }
    public GhostBehaviour Behaviour => behaviour;
    public Transform FlankBase => flankBase;
    public float ScoreValue => scoreValue;

    public SoundClip[] SoundClips => new[] { new SoundClip(consumedClip) };

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    protected virtual async void OnEnable()
    {
        GameManager gameManager = await Toolbox.Instance.GetValueAsync<GameManager>();
        gameManager.AddGhost(this);
    }

    protected virtual void OnDisable()
    {
        if(Toolbox.Instance.TryGetValue<GameManager>(out GameManager gameManager))
        {
            gameManager.RemoveGhost(this);
        }
    }

    public void Consume()
    {
        StartCoroutine(ConsumeCoroutine());
    }

    private IEnumerator ConsumeCoroutine()
    {
        ScoreManager.Instance.AddScore(this);
        SoundManager.Instance.PlaySoundClipAtPoint(this);
        Animator.SetTrigger("Consumed");
        Animator.SetBool("Retreating", true);

        SoundManager.Instance.StopSound();
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(consumedClip.length);
        SoundManager.Instance.RetreatingSound();

        Time.timeScale = 1;
    }
}
