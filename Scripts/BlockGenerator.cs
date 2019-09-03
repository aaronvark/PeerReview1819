using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

    [SerializeField]
    private Block[] _blockCollection;
    [SerializeField]
    private Transform beginPosition;

    public Block GenerateNewBlock() {
        int randomNumber = Random.Range(0, _blockCollection.Length);
        Block block = _blockCollection[randomNumber];
        return block;
    }
}
