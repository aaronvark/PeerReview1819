using System;
using UnityEngine;
using UnityEngine.Events;

public class OnEvent : MonoBehaviour
{
    [SerializeField]
    private EventTrigger eventTrigger = EventTrigger.None;
    //[SerializeField]
    //private bool ignoreSceneUnload = true;
    [SerializeField]
    private bool checkTag = false;
    [SerializeField]
    [Tag]
    private string collisionTag = null;
    [SerializeField]
    private bool triggerOnce = false;

    [SerializeField]
    private UnityEvent unityEvent = new UnityEvent();
    [SerializeField]
    public ColliderEvent colliderEvent = new ColliderEvent();
    [SerializeField]
    public Collider2DEvent collider2DEvent = new Collider2DEvent();
    [SerializeField]
    public CollisionEvent collisionEvent = new CollisionEvent();
    [SerializeField]
    public Collision2DEvent collision2DEvent = new Collision2DEvent();

    private bool ignoreEvent = false;

    public enum EventTrigger
    {
        None,
        OnStart,
        OnDestroy,
        OnEnable,
        OnDisable,
        TriggerEnter,
        TriggerExit,
        TriggerEnter2D,
        TriggerExit2D,
        CollisionEnter,
        CollisionExit,
        CollisionEnter2D,
        CollisionExit2D,
        MouseEnter,
        MouseExit,
        MouseDown,
        MouseUp,
    }

    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> { }
    [Serializable]
    public class Collider2DEvent : UnityEvent<Collider2D> { }
    [Serializable]
    public class CollisionEvent : UnityEvent<Collision> { }
    [Serializable]
    public class Collision2DEvent : UnityEvent<Collision2D> { }

    private bool CheckTag(string tag)
    {
        return !checkTag || collisionTag == tag;
    }

    private void Start()
    {
        HandleGameEvent(EventTrigger.OnStart);
    }

    private void OnDestroy()
    {
        HandleGameEvent(EventTrigger.OnDestroy);
    }

    private void OnEnable()
    {
        HandleGameEvent(EventTrigger.OnEnable);
    }

    private void OnDisable()
    {
        HandleGameEvent(EventTrigger.OnDisable);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.TriggerEnter, other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.TriggerExit, other);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.TriggerEnter2D, other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CheckTag(other.tag))
        {
            HandleGameEvent(EventTrigger.TriggerExit2D, other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.CollisionEnter, collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.CollisionExit, collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.CollisionEnter2D, collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckTag(collision.transform.tag))
        {
            HandleGameEvent(EventTrigger.CollisionExit2D, collision);
        }
    }

    private void OnMouseEnter()
    {
        HandleGameEvent(EventTrigger.MouseEnter);
    }

    private void OnMouseExit()
    {
        HandleGameEvent(EventTrigger.MouseExit);
    }

    private void OnMouseDown()
    {
        HandleGameEvent(EventTrigger.MouseDown);
    }

    private void OnMouseUp()
    {
        HandleGameEvent(EventTrigger.MouseUp);
    }

    private void OnApplicationQuit()
    {
        ignoreEvent = true;
    }

    private void HandleGameEvent(EventTrigger gameEvent, object obj = null)
    {
        if (eventTrigger != gameEvent || ignoreEvent)
            return;

        switch (eventTrigger)
        {
            case EventTrigger.TriggerEnter:
            case EventTrigger.TriggerExit:
                colliderEvent.Invoke(obj as Collider);
                break;
            case EventTrigger.TriggerEnter2D:
            case EventTrigger.TriggerExit2D:
                collider2DEvent.Invoke(obj as Collider2D);
                break;
            case EventTrigger.CollisionEnter:
            case EventTrigger.CollisionExit:
                collisionEvent.Invoke(obj as Collision);
                break;
            case EventTrigger.CollisionEnter2D:
            case EventTrigger.CollisionExit2D:
                collision2DEvent.Invoke(obj as Collision2D);
                break;
            default:
                unityEvent.Invoke();
                break;
        }

        if (triggerOnce && gameEvent != EventTrigger.OnDestroy)
            Destroy(this);
    }
}
