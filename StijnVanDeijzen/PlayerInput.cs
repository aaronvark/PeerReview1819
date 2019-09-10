using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public void GetInput()
    {
        //buttons set in Project settings

        float _inputHor = Input.GetAxis("Player1Hor");
        float _inputVer = Input.GetAxis("Player1Ver");
        float _inputRot = Input.GetAxis("Player1Rot");
        bool _inputSav = Input.GetButtonDown("Player1Sav");
        GameManager.Instance.players[0].GetBlock()?.Move(_inputHor, _inputVer);
        GameManager.Instance.players[0].GetBlock()?.Rotate(_inputRot);
        if (_inputSav) { GameManager.Instance.players[0].SaveBlock(); }

        _inputHor = Input.GetAxis("Player2Hor");
        _inputVer = Input.GetAxis("Player2Ver");
        _inputRot = Input.GetAxis("Player2Rot");
        _inputSav = Input.GetButtonDown("Player2Sav");
        GameManager.Instance.players[1].GetBlock()?.Move(_inputHor, _inputVer);
        GameManager.Instance.players[1].GetBlock()?.Rotate(_inputRot);
        if (_inputSav) { GameManager.Instance.players[1].SaveBlock(); }
    }
}
