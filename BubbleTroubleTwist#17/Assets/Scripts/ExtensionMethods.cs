using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static extensions for methods I often use
/// </summary>
public static class ExtensionMethods
{
    private static BasExtensionHelpers basCurrentHelper = (new GameObject("basCurrentHelper")).AddComponent<BasExtensionHelpers>();

    public static RectTransform LerpRectTransform(this RectTransform rectTransform, Vector3 targetPosition, float speed)
    {
        return basCurrentHelper.ActivateCoroutineLerpRectTransformPositions(rectTransform, targetPosition, speed);
    }

    public static Transform LerpTransform(this Transform currentTransform, Vector3 targetPosition, float speed)
    {
        return basCurrentHelper.ActivateCoroutineLerpTransformPositions(currentTransform, targetPosition, speed);
    }

    public static List<T> ToList<T>(this T[] array) where T : class
    {
        List<T> output = new List<T>();
        output.AddRange(array);
        return output;
    }
}

/// <summary>
/// Helper class for some of my extensions
/// </summary>
public class BasExtensionHelpers : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public RectTransform ActivateCoroutineLerpRectTransformPositions(RectTransform rectTransform, Vector3 targetPosition, float speed)
    {
        if (rectTransform == null) return null;
        StartCoroutine(LerpRectTransformPositions(rectTransform, targetPosition, speed));
        return rectTransform;
    }

    public Transform ActivateCoroutineLerpTransformPositions(Transform currentTransform, Vector3 targetPosition,
        float speed)
    {
        if (currentTransform == null) return null;
        StartCoroutine(LerpTransformPositions(currentTransform, targetPosition, speed));
        return currentTransform;
    }

    private IEnumerator LerpRectTransformPositions(RectTransform rectTransform, Vector3 targetPosition, float speed)
    {
        //The time
        float time = Time.time;

        //Old localPosition of the RectTransform
        Vector3 rectOldLocalPosition = rectTransform.localPosition;

        //While loop 
        while (rectTransform.localPosition != targetPosition)
        {
            //Lerp from rectTransform to targetPosition with speed
            rectTransform.localPosition = Vector3.Lerp(rectOldLocalPosition, targetPosition, (Time.time - time) * speed);

            yield return null;
        }

        yield return new WaitForSeconds(3f);
    }

    private IEnumerator LerpTransformPositions(Transform currentTransform, Vector3 targetPosition, float speed)
    {
        //The Time
        float time = Time.time;

        //old position
        Vector3 transformOldPosition = currentTransform.position;

        //While loop
        while (currentTransform.position != targetPosition)
        {
            currentTransform.position = Vector3.MoveTowards(transformOldPosition, targetPosition, (Time.time - time) * speed);

            yield return null;
        }

        yield return new WaitForSeconds(10f);
    }
}

