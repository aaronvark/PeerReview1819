using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyClass : MonoBehaviour
{
    private BasFSM basFSM;

    private void Start()
    {
        basFSM = new BasFSM(new SomeState());
    }

    private void Update()
    {
        basFSM.Update();
    }
}
