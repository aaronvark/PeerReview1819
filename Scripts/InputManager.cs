using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {

    /// <summary>
    /// current block you're controlling
    /// </summary>
    [SerializeField]
    private Block _currentBlock;
    public Block CurrentBlock {
        get {
            return _currentBlock;
        }
        set {
            _currentBlock = value;
        }
    }
    [SerializeField]
    private BlockGenerator _blockGenerator;


    private void FixedUpdate() {
        if (_currentBlock) {
            if (Input.GetAxis("Horizontal") != 0) {
                CurrentBlock.Move(Input.GetAxis("Horizontal"));
            }
            if (Input.GetKey(KeyCode.X)) {
                CurrentBlock.Rotate(1);
            }
            if (Input.GetKey(KeyCode.Z)) {
                CurrentBlock.Rotate(-1);
            }
        }
    }

    /// <summary>
    /// Updating the current block.
    /// </summary>
    public void UpdateCurrentBlock() {
        GameObject go = _blockGenerator.GenerateNewBlock();
        go = Instantiate<GameObject>(go);
        CurrentBlock = go.GetComponent<Block>();

    }


}
