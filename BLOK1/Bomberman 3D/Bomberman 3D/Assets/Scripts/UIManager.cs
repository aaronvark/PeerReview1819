using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] playerHearts;
    [SerializeField] private Image[] enemy1Hearts;
    [SerializeField] private Image[] enemy2Hearts;
    [SerializeField] private Image[] enemy3Hearts;

    public void DecreaseHearts(int _gameID, int _health)
    {
        if (_gameID == 1)
        {
            playerHearts[_health].enabled = false;
        }
        if (_gameID == 2)
        {
            enemy1Hearts[_health].enabled = false;
        }
        if (_gameID == 3)
        {
            enemy2Hearts[_health].enabled = false;
        }
        if (_gameID == 4)
        {
            enemy3Hearts[_health].enabled = false;
        }
    }
}
