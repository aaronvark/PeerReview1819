using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveler : MonoBehaviour
{
    public float GetScrap { get { return scrap; } }
    [SerializeField]
    private Sprite[] levelSprites;
    [SerializeField]
    private Projectile[] allWeapons;
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField]
    private int levelSpriteChangeModifier;

    private float scrap;
    private Weapon playerGunner;

    // Manages the current sprite progress of the player
    private void Start()
    {
        playerGunner = GetComponent<Weapon>();
    }

    private void Update()
    {
        if (GameManager.Instance.gameIsPlaying)
        {
            // checks the current "level" of the player based on the amount of scrap he has collected
            int _tempFloat = Mathf.RoundToInt(scrap);
            _tempFloat = _tempFloat / levelSpriteChangeModifier;

            GameManager.Instance.playerLevel = _tempFloat < levelSprites.Length ? _tempFloat : (levelSprites.Length - 1);
            if (playerSpriteRenderer.sprite != levelSprites[GameManager.Instance.playerLevel] && GameManager.Instance.playerLevel < levelSprites.Length)
            {
                playerSpriteRenderer.sprite = levelSprites[GameManager.Instance.playerLevel];
                playerGunner.chosenWeapon = allWeapons[GameManager.Instance.playerLevel];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable _tempPick = collision.gameObject.GetComponent<IPickable>();
        if (_tempPick != null)
            scrap = Mathf.Round(scrap + _tempPick.PickItem());
    }
}
