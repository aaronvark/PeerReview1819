using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveler : MonoBehaviour
{
    [SerializeField]
    private Sprite[] levelSprites;
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField]
    private int levelSpriteChangeModifier;

    // Manages the current sprite progress of the player
    void Update()
    {
        int _tempValue = Mathf.RoundToInt(ScoreManager.Instance.getPoints());
        _tempValue = _tempValue / levelSpriteChangeModifier;
        playerSpriteRenderer.sprite = levelSprites[_tempValue];
    }
}
