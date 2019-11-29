using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomCanvas
{
    public CustomCanvas(GameObject canvas)
    {
        //Setup of canvas
        canvas.name = "Canvas";
        canvas.AddComponent<RectTransform>();
        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.AddComponent<GraphicRaycaster>();

        //Setup of event system
        GameObject eventSystem = new GameObject();
        eventSystem.name = "EventSystem";
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }
}
