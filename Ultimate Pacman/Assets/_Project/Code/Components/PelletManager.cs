using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletManager : Singleton<PelletManager>
{
    [SerializeField]
    private string nextScene = "";

    private void Start()
    {
        CheckPellets();
    }

    public void CheckPellets()
    {
        if (Pellet.PelletCount == 0)
        {
            SceneHandler.instance.LoadScene(nextScene);
        }
    }
}
