using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public int amountPlayersAlive;

    [SerializeField] private List<Actor> _actors;

    private static ActorManager _instance;

    public static ActorManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    public void Start()
    {
        _instance = this;
    }

    public void RemoveFromList(Actor actor)
    {
        _actors.Remove(actor);
    }
}
