using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AssetHelper
{
    public static GameObject loadAsset(string path)
    {
        return GameObject.Find(path);
    }
    public static GameObject loadPrefab(string path)
    {
        GameObject myPrefab = Resources.Load<GameObject>(path);

        if (myPrefab == null)
        {
            Debug.LogError("Prefab not found in Resources folder.");
        }

        return myPrefab;
        //// Instantiate the prefab
        //GameObject instantiatedPrefab = GameObject.Instantiate(myPrefab);
        //return instantiatedPrefab;
    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta));
    }

    public static Vector2 DegreeToVector2(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        var cos = Mathf.Cos(radians);
        var sin = Mathf.Sin(radians);
        return new Vector2(cos, sin);
    }
}
