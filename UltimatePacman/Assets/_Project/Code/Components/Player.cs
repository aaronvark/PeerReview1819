using Shapes2D;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
public class Player : MonoBehaviour, ISound
{
    [SerializeField]
    private float animationSpeedMultiplier = 2f;
    [SerializeField]
    private float maxMouthAngle = 60f;
    [SerializeField]
    private AudioClip deathClip = null;
    [SerializeField]
    private string gameOverScene = null;

    private Movement2D movement;
    private Shape shape;
    private int horizontalInput = 0;
    private LerpValue mouthAngle;

    private const float MOUTH_CLOSED_ANGLE = 0;

    public SoundClip[] SoundClips => new[] { new SoundClip(deathClip) };

    private void Awake()
    {
        Toolbox.Instance.Add(this);
    }

    private void Start()
    {
        movement = GetComponent<Movement2D>();
        shape = GetComponent<Shape>();

        mouthAngle = new LerpValue(ChangeMouthAnimationDirection, 40f);
    }

    private float ChangeMouthAnimationDirection(LerpValue _lerpValue)
    {
        return (Mathf.Approximately(_lerpValue.Current, maxMouthAngle)) ? MOUTH_CLOSED_ANGLE : maxMouthAngle;
    }

    private void Update()
    {
        // Stores the input value for use in the fixed update for correct physics handling.
        horizontalInput = Mathf.RoundToInt(Input.GetAxis("Horizontal"));

        AnimateMouth();
    }

    private void FixedUpdate()
    {
        movement.Rotate(horizontalInput);
        movement.Move(transform.right);
    }

    private void AnimateMouth()
    {
        // Update mouth angle
        mouthAngle.Speed = movement.moveSpeed * animationSpeedMultiplier;
        mouthAngle.Update();

        // Set new mouth angle
        shape.settings.startAngle = mouthAngle.Current;
        shape.settings.endAngle = 360f - mouthAngle.Current;
    }

    // Decide what to do when hit and when the collision came from a ghost
    public void HandleGhostCollision(Collision2D collision)
    {
        Ghost ghost = collision.transform.GetComponent<Ghost>();
        if (!ghost)
            return;

        AnimatorStateInfo currentState = ghost.Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsTag("Consumable"))
        {
            ghost.Consume();
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        // Pause the game
        Time.timeScale = 0;

        SoundManager.Instance.StopSound();
        yield return new WaitForSecondsRealtime(1);

        // Play death sound
        SoundManager.Instance.PlaySoundClipAtPoint(this);

        // Rotate the player to look upwards
        var rot = transform.rotation;
        rot.eulerAngles = Vector3.forward * 90;
        transform.rotation = rot;

        // Animate the player disappearing
        const float animationDuration = 4f / 5f;
        float animationTime = animationDuration;
        while(animationTime > 0f)
        {
            float angle = Mathf.Lerp(180, 0, animationTime / animationDuration);
            shape.settings.startAngle = angle;
            shape.settings.endAngle = 360 - angle;

            animationTime -= Time.fixedDeltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().enabled = false;

        // Wait for clip to end
        yield return new WaitForSecondsRealtime(deathClip.length - animationDuration);

        // Stop pause and go to Game Over
        Time.timeScale = 1;
        SceneHandler.Instance.LoadScene(gameOverScene);
    }

    private void OnDestroy()
    {
        Toolbox.Instance.Remove(this);
    }
}
