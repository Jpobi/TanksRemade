using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Item
{
    public Weapon wp;
    public string weaponClassName;

    protected override void ApplyTo(GameObject go)
    {
        go.GetComponentInChildren<Shooting>().SwapWeapon(wp);
    }


    private void Start()
    {
        InitializeWeapon();
    }

    private void InitializeWeapon()
    {
        Type weaponType = Type.GetType(weaponClassName);

        if (weaponType != null && typeof(Weapon).IsAssignableFrom(weaponType))
        {
            wp = (Weapon)gameObject.AddComponent(weaponType);
        }
        else
        {
            Debug.LogError("Weapon subclass with the specified name not found.");
        }
    }
}


