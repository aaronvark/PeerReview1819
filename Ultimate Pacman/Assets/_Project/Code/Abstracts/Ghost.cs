using Shapes2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Movement2D))]
[RequireComponent(typeof(Animator))]
public abstract class Ghost : MonoBehaviour, IScore
{
    [SerializeField]
    private Color ghostColor = new Color(2f / 30f, .2f, 1f);
    [SerializeField]
    private int regeneratingLoops = 2;
    [SerializeField]
    private float scoreValue = 200f;

    protected Vector2? target = null;
    protected Movement2D movement;
    protected Animator animator;
    protected Collider2D[] colliders;
    protected Coroutine regenerateCoroutine = null;

    [SerializeField]
    [HideInInspector]
    public Body body { get; protected set; }

    private const float onTargetDistance = 0.1f;
    private const float regenerateLoopDistance = 0.2f;

    public float ScoreValue => scoreValue;

    public State state { get; protected set; } = State.Default;

    public enum State
    {
        Default,
        Vulnerable,
        Regenerating,
    }

    protected virtual void Awake()
    {
        movement = GetComponent<Movement2D>();
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
        body = new Body(transform);
    }

    protected virtual void OnEnable()
    {
        GameManager.Instance.AddGhost(this);
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.RemoveGhost(this);
    }

    public void SetVulnerable(bool _vulnerable)
    {
        State targetState = (_vulnerable) ? State.Vulnerable : State.Default;

        switch (state)
        {
            case State.Regenerating:
            case State s when s == targetState:
                return;
            default:
                state = targetState;
                break;
        }

        switch (state)
        {
            case State.Vulnerable:
                body.SetColorVulnerable();
                body.ToggleHiddenParts();
                movement.moveSpeed /= 2;
                break;
            default:
                body.SetColor(ghostColor);
                body.ToggleHiddenParts();
                movement.moveSpeed *= 2;
                break;
        }
    }

    public void Consume()
    {
        ScoreManager.Instance.AddScore(this);

        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        body.SetColor(new Color(0, 0, 0, 0));
        body.ToggleHiddenParts();
        movement.moveSpeed *= 2;
        SetRegenerateTarget();
        state = State.Regenerating;
    }

    private void SetRegenerateTarget()
    {
        Collider2D borderCollider = GameManager.Instance.collider;
        Vector2 closestBoundsPoint = borderCollider.ClosestPoint(transform.position);
        Vector2 newTargetDirection = (closestBoundsPoint - (Vector2)transform.position).normalized;
        Vector2 newTarget = closestBoundsPoint + newTargetDirection;
        target = newTarget;
    }

    private IEnumerator Regenerate()
    {
        Vector2 regenerationPosition = transform.position;
        int loops = regeneratingLoops * 2;
        Vector2 direction = Vector2.right;

        while (loops >= 0)
        {
            movement.Move(direction);
            yield return null;

            if (Vector2.Distance(regenerationPosition, transform.position) >= regenerateLoopDistance)
            {
                transform.position = regenerationPosition + direction * regenerateLoopDistance;
                yield return null;

                direction *= -1;
                loops--;
                yield return null;
            }
        }

        while (Vector2.Distance(regenerationPosition, transform.position) >= onTargetDistance)
        {
            movement.Move(direction);
            yield return null;
        }

        transform.position = regenerationPosition;

        body.SetColor(ghostColor);

        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }

        state = State.Default;
        regenerateCoroutine = null;

        yield return null;
    }

    protected virtual void Update()
    {
        switch (state)
        {
            case State.Regenerating:
                if (regenerateCoroutine == null &&
                    target != null &&
                    Vector2.Distance(transform.position, (Vector2)target) <= onTargetDistance)
                {
                    target = null;
                    regenerateCoroutine = StartCoroutine(Regenerate());
                }
                break;
            default:
                target = Player.Instance.transform.position;
                break;
        }
    }

    protected virtual void OnValidate()
    {
        if (body != null)
            body.SetColor(ghostColor);
    }

    protected virtual void Reset()
    {
        body = new Body(transform);
    }

    [Serializable]
    public class Body
    {
        [SerializeField]
        private List<Transform> colorParts = new List<Transform>();
        [SerializeField]
        private List<GameObject> hiddenParts = new List<GameObject>();
        [SerializeField]
        private List<Transform> blinkingParts = new List<Transform>();

        private readonly Color vulnerableColor = new Color(2f / 30f, .2f, 1f);
        private readonly Color blinkingColor1 = Color.white;
        private readonly Color blinkingColor2 = new Color(1f, .2f, 2f / 30f);

        public Body(Transform _transform)
        {
            colorParts.Add(_transform.Find("Head"));
            colorParts.Add(_transform.Find("Head/Body"));
            foreach (Transform spike in _transform.Find("Head/Body"))
            {
                colorParts.Add(spike);
            }

            hiddenParts.Add(_transform.Find("Head/Eye Left/Pupil").gameObject);
            hiddenParts.Add(_transform.Find("Head/Eye Right/Pupil").gameObject);
            hiddenParts.Add(_transform.Find("Head/Mouth").gameObject);

            blinkingParts.Add(_transform.Find("Head/Eye Left"));
            blinkingParts.Add(_transform.Find("Head/Eye Right"));
            foreach (Transform mouthPiece in _transform.Find("Head/Mouth"))
            {
                blinkingParts.Add(mouthPiece);
            }
        }

        public void Reset()
        {

        }

        public void SetColor(Color _color)
        {
            foreach (var colorPart in colorParts)
            {
                Shape shape = colorPart.GetComponent<Shape>();
                shape.settings.fillColor = _color;
            }
        }

        public void SetColorVulnerable()
        {
            SetColor(vulnerableColor);
        }

        public void ToggleHiddenParts()
        {
            for (int i = 0; i < hiddenParts.Count; i++)
            {
                hiddenParts[i].SetActive(!hiddenParts[i].activeSelf);
            }
        }

        public void Blink(bool _white)
        {
            if (_white)
            {
                SetColor(blinkingColor1);
                foreach (Transform blinkingPart in blinkingParts)
                {
                    Shape shape = blinkingPart.GetComponent<Shape>();
                    shape.settings.fillColor = blinkingColor2;
                }
            }
            else
            {
                SetColor(vulnerableColor);
                foreach (Transform blinkingPart in blinkingParts)
                {
                    Shape shape = blinkingPart.GetComponent<Shape>();
                    shape.settings.fillColor = Color.white;
                }
            }
        }
    }
}
