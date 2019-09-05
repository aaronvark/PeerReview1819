using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPellet : Pellet
{
    [SerializeField]
    private Vector2 sizeRange = new Vector2(.25f, .4f);
    [SerializeField]
    private float animationSpeed = 4f;

    private LerpValue sizeLerp;

    private void Start()
    {
        sizeLerp = new LerpValue(ChangeSizeDirection, sizeRange.x, InterpolationMethod.Cosine);
        sizeLerp.Speed = animationSpeed;
    }

    private float ChangeSizeDirection(LerpValue _lerpValue)
    {
        if (_lerpValue.Current == sizeRange.y)
        {
            return sizeRange.x;
        }
        else
        {
            return sizeRange.y;
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
