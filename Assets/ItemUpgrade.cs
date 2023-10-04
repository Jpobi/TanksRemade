using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : Item
{
    protected override void ApplyTo(GameObject go)
    {
        go.GetComponentInChildren<Shooting>().weapon.damage += 25;
    }
}
