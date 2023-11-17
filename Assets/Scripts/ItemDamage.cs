using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamage : Item
{
    protected override void ApplyTo(GameObject go)
    {
        go.GetComponentInChildren<Shooting>().weapon.damage += 15;
    }
}
