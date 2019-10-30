using Shapes2D;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
public class Player : Singleton<Player>
{
    [SerializeField]
    private float animationSpeedMultiplier = 2f;
    [SerializeField]
    private float maxMouthAngle = 60f;
    [SerializeField]
    private string gameOverScene = null;

    private Movement2D movement;
    private Shape shape;
    private int horizontalInput = 0;
    private LerpValue mouthAngle;

    private const float MOUTH_CLOSED_ANGLE = 0;

    private void Start()
    {
        movement = GetComponent<Movement2D>();
        shape = GetComponent<Shape>();

        mouthAngle = new LerpValue(ChangeMouthAnimationDirection);
    }

    private float ChangeMouthAnimationDirection(LerpValue _lerpValue)
    {
        return (Mathf.Approximately(_lerpValue.Current, maxMouthAngle)) ? MOUTH_CLOSED_ANGLE : maxMouthAngle;
    }

    private void Update()
    {
        // Stores the input value for use in the fixed update for correct physics handling.
        horizontalInput = Mathf.RoundToInt(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

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
    public void HandleGhostCollision(Collision2D _collision)
    {
        Ghost ghost = _collision.transform.GetComponent<Ghost>();
        if (!ghost)
            return;

        AnimatorStateInfo currentState = ghost.animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsTag("Consumable"))
        {
            ghost.Consume();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        SceneHandler.Instance.LoadScene(gameOverScene);
    }
}
