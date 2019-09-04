using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enables easy creation of weapon components
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public float rateOfFire;
    public GameObject weaponEntity;
}

