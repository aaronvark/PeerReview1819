using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour {

    [SerializeField] private List <DestructableWall> _destructableWall;

    private static WallManager _instance;

    public static WallManager Instance {
        get {
            return _instance;
        }
        set {
            _instance = value;
        }
    }
    
    public void Start() {
        _instance = this;
    }

    public void RemoveFromList(DestructableWall destructableWall) {
        _destructableWall.Remove(destructableWall);
    }
}
