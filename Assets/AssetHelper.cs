using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AssetHelper
{
    public static Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta));
    }

    public static Vector3 rotate(Vector3 v, float delta)
    {
        float x = v.x * Mathf.Cos(delta) - v.z * Mathf.Sin(delta);
        float y = v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta);

        return new Vector3(x, y, v.z);
    }

    public static void ShowText(Vector3 location, Color textColor,int textSize, string textValue)
    {
        GameObject textPrefab = Resources.Load<GameObject>("Text");

        if (textPrefab == null)
        {
            Debug.LogError("Prefab with name 'Text' not found in Resources folder.");
            return;
        }

        GameObject textObject = Object.Instantiate(textPrefab, location, Quaternion.identity);
        TextMesh textMesh = textObject.GetComponent<TextMesh>();

        if (textMesh != null)
        {
            textMesh.color = textColor;
            textMesh.text = textValue;
            textMesh.fontSize = textSize;
            Object.Destroy(textObject, 1f);
        }
        else
        {
            Debug.LogError("TextMesh component not found on the instantiated object.");
        }
    }

    public static Vector2 DegreeToVector2(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        var cos = Mathf.Cos(radians);
        var sin = Mathf.Sin(radians);
        return new Vector2(cos, sin);
    }
}
