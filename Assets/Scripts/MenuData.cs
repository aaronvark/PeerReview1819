using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MenuData", menuName = "MenuDataObject")]

public class MenuData : ScriptableObject
{
    public GameObject MenuGroup;
    public GameObject CreditGroup;
}
