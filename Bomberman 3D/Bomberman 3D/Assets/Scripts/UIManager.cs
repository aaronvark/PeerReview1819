using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] playerHearts;
    //[SerializeField] private Image[] enemy1Hearts;
    //[SerializeField] private Image[] enemy2Hearts;
    //[SerializeField] private Image[] enemy3Hearts;

    public void DecreaseHearts(int gameID, int health)
    {
        Debug.Log("heart decreased");
        if (gameID == 1)
        {
            playerHearts[health].enabled = false;
            Debug.Log("heart decreased");
        }
        else if (gameID == 2)
        {
            playerHearts[health].enabled = false;
        }
        else if (gameID == 3)
        {
            playerHearts[health].enabled = false;
        }
        else if (gameID == 4)
        {
            playerHearts[health].enabled = false;
        }
    }
}
