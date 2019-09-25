using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInteractable
{
    public void Interact(GameObject _interactor)
    {
        GemManager.Instance.RemoveGem(gameObject);
    }
}
