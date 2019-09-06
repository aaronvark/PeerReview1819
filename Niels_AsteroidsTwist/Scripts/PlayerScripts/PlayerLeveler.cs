using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveler : MonoBehaviour
{
    [SerializeField]
    private Sprite[] levelSprites;
    [SerializeField]
    private Weapon[] allWeapons;
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField]
    private int levelSpriteChangeModifier;

    private PlayerFire playerGunner;

    // Manages the current sprite progress of the player
    private void Start()
    {
        playerGunner = GetComponent<PlayerFire>();
    }

    void Update()
    { 
        int _tempValue = Mathf.RoundToInt(ScoreManager.Instance.getPoints());
        _tempValue = _tempValue / levelSpriteChangeModifier;
        if (playerSpriteRenderer.sprite != levelSprites[_tempValue] && _tempValue < levelSprites.Length)
        {
            playerSpriteRenderer.sprite = levelSprites[_tempValue];
            playerGunner.chosenWeapon = allWeapons[_tempValue];
        }
    }
}
