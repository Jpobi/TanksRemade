using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Weapon weapon;
    public GameObject firePoint;

    void Start()
    {
        weapon = gameObject.AddComponent<BasicWeapon>();
        weapon.Initialize(firePoint.transform);
        gameObject.GetComponent<SpriteRenderer>().sprite = weapon.weaponSprite;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }

    public void SwapWeapon(string weaponClassName)
    {
        Type weaponType = Type.GetType(weaponClassName);

        if (weaponType != null && typeof(Weapon).IsAssignableFrom(weaponType))
        {
            Destroy(weapon);
            weapon = (Weapon)gameObject.AddComponent(weaponType);
            weapon.Initialize(firePoint.transform);
            gameObject.GetComponent<SpriteRenderer>().sprite = weapon.weaponSprite;
        }
        else
        {
            Debug.LogError("Weapon subclass with the specified name not found.");
        }
    }
}
