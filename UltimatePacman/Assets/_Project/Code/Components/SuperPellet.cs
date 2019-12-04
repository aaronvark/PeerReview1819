using UnityEngine;

public class SuperPellet : Pellet
{
    [SerializeField]
    private float superDuration = 8f;
    [SerializeField]
    private Vector2 sizeRange = new Vector2(.25f, .4f);
    [SerializeField]
    private float animationSpeed = 4f;

    private LerpValue sizeLerp; // Grows and Shrinks the value for the pellet size.

    private void Start()
    {
        sizeLerp = new LerpValue(ChangeSizeDirection, sizeRange.x, InterpolationMethod.Cosine)
        {
            Speed = animationSpeed
        };

        transform.localScale = Vector2.one * sizeLerp.Current;
    }

    private float ChangeSizeDirection(LerpValue _lerpValue)
    {
        return (_lerpValue.Current == sizeRange.y) ? sizeRange.x : sizeRange.y;
    }

    public override void Consume()
    {
        base.Consume();

        if(Toolbox.Instance.TryGetValue<GameManager>(out GameManager gameManager)) 
        {
            gameManager.SetGhostsVulnerable(superDuration);
        }
    }

    private void Update()
    {
        AnimateSize();
    }

    private void AnimateSize()
    {
        sizeLerp.Update();

        transform.localScale = Vector2.one * sizeLerp.Current;
    }
}
