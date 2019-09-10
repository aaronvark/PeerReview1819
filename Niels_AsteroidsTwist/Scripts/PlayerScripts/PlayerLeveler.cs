using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveler : MonoBehaviour
{
    public float GetScrap { get { return scrap; } }
    [SerializeField]
    private Sprite[] levelSprites;
    [SerializeField]
    private Weapon[] allWeapons;
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;
    [SerializeField]
    private int levelSpriteChangeModifier;

    private float scrap;
    private PlayerFire playerGunner;

    // Manages the current sprite progress of the player
    private void Start()
    {
        playerGunner = GetComponent<PlayerFire>();
    }

    private void Update()
    {
        int _tempValue = Mathf.RoundToInt(scrap);
        _tempValue = _tempValue / levelSpriteChangeModifier;
        if (playerSpriteRenderer.sprite != levelSprites[_tempValue] && _tempValue < levelSprites.Length)
        {
            playerSpriteRenderer.sprite = levelSprites[_tempValue];
            playerGunner.chosenWeapon = allWeapons[_tempValue];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //float flap = scrap == 1 ? 0 : (true ? 1 : 2);
        //if (scrap == 1)
        //{
        //    flap = 0;
        //} else {
        //    if (true)
        //    {
        //        flap = 1;

        //    }
        //    else
        //    {
        //        flap = 2;
        //    }
        //}
        IPickable _tempPick = collision.gameObject.GetComponent<IPickable>();
        if (_tempPick != null)
            scrap = Mathf.Round(scrap + _tempPick.PickItem());
    }
}
