using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player class with all player related functionality
/// </summary>

[System.Serializable]
public class PlayerData
{
    public int id;
    public string horizontalAxis;
    public string verticalAxis;
    public int level;
}

public class Player : AbstractAvatarClass 
{
    public List<PlayerData> players;

    public enum Players
    {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3
    }

    public Players currentPlayer = Players.Player1;

    public Transform firePoint;
    public Projectile currentProjectile;
    public float cooldown;
    public float timeBetween;
    public string currentProjectileName;

    public WeaponData usingWeaponData;

    Weapon currentWeapon;
    PlayerData currentPlayerData;
    Rigidbody rBody;

    void Awake()
    {
        //currentWeapon = new Weapon(currentProjectileName, firePoint, currentProjectile, damage);
        //Create a weapon and give its weapon data
        currentWeapon = new Weapon();
        currentWeapon.thisWeaponData = usingWeaponData;
        rBody = GetComponent<Rigidbody>();       
    }

    public override void Start()
    {
        base.Start();
        if (currentPlayer == Players.Player1)
        {
            //Search players for the current selected player ( Lamba )
            currentPlayerData = players.Find(p => p.id.Equals((int)currentPlayer));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentWeapon.WeaponReady()) 
            Fire();
    }

    private void FixedUpdate()
    {
        if(currentPlayerData != null)
            Movement();
    }

    private void Fire()
    {
        currentWeapon.objectPooler = objectPooler;
        StartCoroutine(currentWeapon.WaitForCooldown(cooldown, timeBetween));
    }
    
    private void Movement()
    {
        float _vertical = Input.GetAxis(currentPlayerData.verticalAxis);
        float _horizontal = Input.GetAxis(currentPlayerData.horizontalAxis);
        rBody.velocity = new Vector3(_horizontal * speed, rBody.velocity.y, _vertical * speed);
    }
}
