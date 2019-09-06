using Shapes2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement2D))]
[RequireComponent(typeof(Animator))]
public abstract class Ghost : MonoBehaviour, IScore
{
    protected Transform target = null;
    protected Movement2D movement;
    protected Animator animator;

    [SerializeField]
    private Color ghostColor = new Color(2f / 30f, .2f, 1f);
    [SerializeField]
    private float scoreValue = 200f;

    [HideInInspector]
    [SerializeField]
    private List<Transform> colorParts = new List<Transform>();
    [HideInInspector]
    [SerializeField]
    private List<GameObject> hiddenParts = new List<GameObject>();
    [HideInInspector]
    [SerializeField]
    private List<Transform> blinkingParts = new List<Transform>();

    public float ScoreValue => scoreValue;

    public bool Vulnerable { get; private set; } = false;

    private static readonly Color vulnerableColor = new Color(2f / 30f, .2f, 1f);
    private static readonly Color blinkingColor1 = Color.white;
    private static readonly Color blinkingColor2 = new Color(1f, .2f, 2f / 30f);

    protected virtual void Awake()
    {
        target = Player.Instance.transform;
        movement = GetComponent<Movement2D>();
        animator = GetComponent<Animator>();
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
        if (Vulnerable == _vulnerable)
            return;

        Vulnerable = _vulnerable;

        if (_vulnerable)
        {
            SetColor(vulnerableColor);
            ToggleHiddenParts();
            movement.moveSpeed /= 2;
        }
        else
        {
            ResetColor();
            ToggleHiddenParts();
            movement.moveSpeed *= 2;
        }
    }

    public void Consume()
    {
        ScoreManager.Instance.AddScore(ScoreValue);
    }

    public void ResetColor()
    {
        SetColor(ghostColor);
    }

    private void ToggleHiddenParts()
    {
        for (int i = 0; i < hiddenParts.Count; i++)
        {
            hiddenParts[i].SetActive(!hiddenParts[i].activeSelf);
        }
    }

    private void SetColor(Color _color)
    {
        foreach (var colorPart in colorParts)
        {
            Shape shape = colorPart.GetComponent<Shape>();
            shape.settings.fillColor = _color;
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

    private void OnValidate()
    {
        ResetColor();
    }

    private void Reset()
    {
        colorParts.Add(transform.Find("Head"));
        colorParts.Add(transform.Find("Head/Body"));
        foreach (Transform spike in transform.Find("Head/Body"))
        {
            colorParts.Add(spike);
        }

        hiddenParts.Add(transform.Find("Head/Eye Left/Pupil").gameObject);
        hiddenParts.Add(transform.Find("Head/Eye Right/Pupil").gameObject);
        hiddenParts.Add(transform.Find("Head/Mouth").gameObject);

        blinkingParts.Add(transform.Find("Head/Eye Left"));
        blinkingParts.Add(transform.Find("Head/Eye Right"));
        foreach (Transform mouthPiece in transform.Find("Head/Mouth"))
        {
            blinkingParts.Add(mouthPiece);
        }
    }
}
