using Shapes2D;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
public class Player : Singleton<Player>
{
    [SerializeField]
    private float animationSpeedMultiplier = 2f;
    [SerializeField]
    private float maxMouthAngle = 60f;

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
        if (Mathf.Approximately(_lerpValue.Current, maxMouthAngle))
        {
            return MOUTH_CLOSED_ANGLE;
        }
        else
        {
            return maxMouthAngle;
        }
    }

    private void Update()
    {
        // Stores the input for use in the fixed update for correct physics handling
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
        mouthAngle.Speed = movement.moveSpeed * animationSpeedMultiplier;
        mouthAngle.Update();

        shape.settings.startAngle = mouthAngle.Current;
        shape.settings.endAngle = 360f - mouthAngle.Current;
    }

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
        SceneHandler.Instance.LoadScene("Game Over"); // FIX: This is a magic number!
    }

    public void TestMethod(Collider2D collider)
    {
        Debug.Log(collider.name);
    }
}
