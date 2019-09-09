using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSdisplay : MonoBehaviour
{
    public float averageFrameRate;
    public UnityEngine.UI.Text displayText;

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        averageFrameRate = (int)current;
        displayText.text = averageFrameRate.ToString() + " FPS";
    }
}
