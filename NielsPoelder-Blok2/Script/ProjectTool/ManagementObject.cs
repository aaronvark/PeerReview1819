using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagementObject", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]

public class ManagementObject : ScriptableObject
{
    public string itemName;
    public ManagementStatus itemStatus;
    public ManagementType itemType;
    [TextArea]
    public string itemDescription;
    // Assigned person kan je ook nog toevoegen als string

}

public enum ManagementStatus
{
    ToDo,
    Completed
}

public enum ManagementType
{
    Art,
    UI,
    Mechanic,
    Bug
}