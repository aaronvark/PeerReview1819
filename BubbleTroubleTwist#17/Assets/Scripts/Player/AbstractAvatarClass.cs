using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System;

public delegate void EntityDataHandler();


/// <summary>
/// Base class for all avatars in the game
/// </summary>
public abstract class AbstractAvatarClass : MonoBehaviour, IDamagable<int>
{
    //public ObjectPooler objectPooler;
    /*
    public enum Players
    {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3
    }

    public Players currentPlayer = Players.Player1;
    */
    //public PlayerData currentPlayerData;
    public event EntityDataHandler entityDataHandler;
    
    public int damage { get; protected set; }
    public float speed { get; protected set; }
    public int health { get; protected set; }

    public event OnDeath OnDeathHandler;
    public Rigidbody rBody;

    public PlayerData entityStats;

    public virtual void Start()
    {
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
        //throw new System.NotImplementedException();
    }

    public virtual bool TakeDamage(int _damage)
    {
        if (health > _damage) { health -= _damage; return true; } else { OnDeathHandler(); return false; }
    }
}
