using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// static extensions for methods I often use
/// </summary>
public static class ExtensionMethods
{
    public static RectTransform LerpRectTransform(this RectTransform rectTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
    {
        return ActivateCoroutineLerpRectTransformPositions(rectTransform, owner, targetPosition, speed);
    }

    public static Transform LerpTransform(this Transform currentTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
    {
        return ActivateCoroutineLerpTransformPositions(currentTransform, owner, targetPosition, speed);
    }

    public static List<T> ToList<T>(this T[] array) where T : class
    {
        List<T> output = new List<T>();
        output.AddRange(array);
        return output;
    }

    public static RectTransform ActivateCoroutineLerpRectTransformPositions(RectTransform rectTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
    {
        if (rectTransform == null) return null;
        try
        {
            owner.StartCoroutine(ExtensionHelpers.LerpRectTransformPositions(rectTransform, targetPosition, speed));
        }
        catch (Exception e)
        {

        }
        return rectTransform;
    }

    public static Transform ActivateCoroutineLerpTransformPositions(Transform currentTransform, MonoBehaviour owner, Vector3 targetPosition,
        float speed)
    {
        if (currentTransform == null) return null;

        try
        {
            owner.StartCoroutine(ExtensionHelpers.LerpTransformPositions(currentTransform, targetPosition, speed));
        }
        catch(Exception e)
        {

        }
        return currentTransform;
    }
}




