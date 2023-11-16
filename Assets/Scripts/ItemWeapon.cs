using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    public string weaponClassName;

    protected override void ApplyTo(GameObject go)
    {
        go.GetComponentInChildren<Shooting>().SwapWeapon(weaponClassName);
    }

}


