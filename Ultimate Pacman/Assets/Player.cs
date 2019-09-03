using Shapes2D;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private float animationSpeedMultiplier = 2f;
    [SerializeField]
    private float maxMouthAngle = 60f;

    private Rigidbody2D rigidbody;
    private Shape shape;
    private int horizontalInput = 0;
    private LerpValue mouthAngle;

    private const float MOUTH_CLOSED_ANGLE = 0;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        shape = GetComponent<Shape>();

        mouthAngle = new LerpValue(Thing);
    }

    private float Thing(LerpValue _lerpValue) {
        if (Mathf.Approximately(_lerpValue.Current, maxMouthAngle)) {
            return MOUTH_CLOSED_ANGLE;
        }
        else {
            return maxMouthAngle;
        }
    }

    private void Update()
    {
        // Stores the input for use in the fixed update for correct physics handling
        horizontalInput = Mathf.RoundToInt(Input.GetAxis("Horizontal"));

        AnimateMouth();
    }

    private void FixedUpdate() {
        float _deltaTime = Time.deltaTime;

        // Rotates the Player
        float _currentRotation = rigidbody.rotation;
        float _rotationChange = horizontalInput * rotationSpeed * _deltaTime;
        rigidbody.SetRotation(_currentRotation + _rotationChange);

        // Moves the Player
        Vector2 _currentPosition = rigidbody.position;
        Vector2 _positionChange = (Vector2)transform.right * moveSpeed * _deltaTime;
        rigidbody.MovePosition(_currentPosition + _positionChange);
    }

    private void AnimateMouth() {
        mouthAngle.Speed = moveSpeed * animationSpeedMultiplier;
        mouthAngle.Update();

        shape.settings.startAngle = mouthAngle.Current;
        shape.settings.endAngle = 360f - mouthAngle.Current;
    }
}
