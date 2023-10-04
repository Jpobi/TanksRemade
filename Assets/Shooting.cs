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
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }

    public void SwapWeapon(Weapon wp)
    {
        //TODO: Swap weapon sprite
        Destroy(weapon);
        weapon = wp;
        weapon.Initialize(firePoint.transform);
    }
}
