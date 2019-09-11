using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System;


public delegate void OnDataUpdate(PlayerData stats);
/// <summary>
/// Base class for all avatars in the game
/// </summary>
public abstract class AbstractAvatarClass : MonoBehaviour, IDamagable<int>
{
    public int damage { get; protected set; }
    public float speed { get; protected set; }
    public int health { get; protected set; }

    public event OnDeath OnDeathHandler;
    public Rigidbody rBody;
    public OnDataUpdate OnDataUpdateHandler;
    public PlayerData playerInput;
    //public StatsBase entityStats;

    public virtual void Start()
    {   
        if(GetComponent<PlayerInput>())
            playerInput = GetComponent<PlayerInput>().playerInput;
        speed = 5f;
        OnDeathHandler += OnDeath;
        //Search players for the current selected player ( Lamba )
        //currentPlayerData = PlayerManager.Instance.players?.Find(p => p.id.Equals((int)currentPlayer));

        if (rBody == null)
            rBody = GetComponent<Rigidbody>();
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public virtual bool TakeDamage(int _damage)
    {
        if (health > _damage) { health -= _damage; return true; } else { OnDeathHandler(); return false; }
    }
}
