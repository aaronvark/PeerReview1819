using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enables easy creation of weapon components
[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
public class Projectile : ScriptableObject
{
    public string weaponName;
    public float rateOfFire;
    public GameObject weaponEntity;
}

