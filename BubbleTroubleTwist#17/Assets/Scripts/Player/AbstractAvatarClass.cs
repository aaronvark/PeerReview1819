using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;
using System;

/// <summary>
/// Base class for all avatars in the game
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class AbstractAvatarClass : MonoBehaviour, IDamagable<int>
{
    /// <summary>
    /// Damage the avatar deals
    /// </summary>
    private int damage = 1;
    public int Damage { get => damage; set => damage = value; }

    /// <summary>
    /// Speed the avatar has
    /// </summary>
    public float Speed { get; protected set; }

    /// <summary>
    /// Health the avatar has
    /// </summary>
    public int Health { get; protected set; }

    /// <summary>
    /// OnDeath event so we can subscribe and call all death related methods
    /// </summary>
    public event OnDeath OnDeathHandler;

    /// <summary>
    /// Rigibody so we can use unity physics
    /// </summary>
    public Rigidbody RBody { get; protected set; }


    /// <summary>
    /// Property where we store the player his input
    /// </summary>
    public PlayerData PlayerInput { get; protected set; }

    /* Comment: Ik wilde graag voor de input een generieke manier zoeken zodat
* de speler als zowel de enemy hier zijn input uit kan halen. Nu staat
* er een "PlayerData" field die er eigenlijk niet hoort. Het gaat in deze class
* namelijk over alle avatars in de game en PlayerData is specefiek voor de speler.
* Helaas kon ik in de tijd die ik voor dit probleem wilde besteden niet een betere oplossing
* vinden. 
*/

    /// <summary>
    /// Virtual Start method / this method can be overriden
    /// </summary>
    public virtual void Start()
    {
        //Check whenever this avatar instance is a player 
        if (GetComponent<PlayerInput>())
        {
            //Set the player his input data
            PlayerInput = GetComponent<PlayerInput>().playerInput;
        }
        else
        {
            //Its not a player :D 
        }

        //We always need speed 
        if (Speed == 0)
        {
            Speed = 10f;
        }

        //Subscribe the OnDeath method
        OnDeathHandler += OnDeath;

        //Search players for the current selected player ( Lamba )
        //currentPlayerData = PlayerManager.Instance.players?.Find(p => p.id.Equals((int)currentPlayer));

        //We always need an Rigidbody
        if (RBody == null)
            RBody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// OnDeath method where we deactivate the avatar
    /// </summary>
    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Decreases health with damage, if we can handle the amount of damage comming, if not we call OnDeathHandler
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    public virtual bool TakeDamage(int _damage)
    {
        if (Health > _damage) { Health -= _damage; return true; } else { OnDeathHandler(); return false; }
    }



    /// <summary>
    /// When this enemy gets destoryed we need to unsubscribe 
    /// </summary>
    private void OnDestroy()
    {
        OnDeathHandler -= OnDeath;
    }
}
