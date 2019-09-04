using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] _blockCollection;
   
    public GameObject GenerateNewBlock() {
        int randomNumber = Random.Range(0, _blockCollection.Length);
        GameObject block = _blockCollection[randomNumber];
        Debug.Log(block);
        return block;
    }
}
