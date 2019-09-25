using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventScript : MonoBehaviour
{
    [SerializeField] UnityEvent onEvent;

    public void OnEvent()
    {
        onEvent.Invoke();
    }
}
