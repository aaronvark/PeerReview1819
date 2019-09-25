using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSdisplay : MonoBehaviour
{

    [SerializeField] private Text displayText;
    public Text DisplayText { get => displayText; set => displayText = value; }

    private float AverageFrameRate { get; set; }

    private void Awake()
    {
        //  DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        AverageFrameRate = (int)current;
        DisplayText.text = AverageFrameRate.ToString() + " FPS";
    }
}
