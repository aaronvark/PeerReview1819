using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour, IInteractable
{
    public void Interact(GameObject _interactor = null)
    {
        LemmingManager.Instance.RemoveLemming(_interactor);
    }
}
