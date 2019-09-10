using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInteractable
{
    public enum GemType { Basic, Special }
    GemType type = GemType.Basic;

    public void Interact(GameObject _interactor)
    {
        UIManager.Instance.UpdateGemItem(0, 1);
        Destroy(gameObject);
    }
}
