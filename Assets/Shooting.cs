using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Weapon weapon;
    public GameObject firePoint;
    public SliderBar cooldownBar;
    public float cooldown = 3f;
    public float currentCooldown;

    void Start()
    {
        weapon = gameObject.AddComponent<BasicWeapon>();
        weapon.Initialize(firePoint.transform);
        gameObject.GetComponent<SpriteRenderer>().sprite = weapon.weaponSprite;
        currentCooldown = cooldown;
        cooldownBar.SetMaxValue(cooldown);
        cooldownBar.SetValue(currentCooldown);
    }

    void Update()
    {
        var parent = transform.parent.gameObject;
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            if (parent.tag != "Enemy" && Input.GetButtonDown("Fire1"))
            {
                AssetHelper.ShowText(transform.position+new Vector3(0,1.2f,0), Color.white,20, "Arma en Cooldown");
            }
            cooldownBar.SetValue(currentCooldown);
            return;
        }

        if (parent.tag != "Enemy" && Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
            currentCooldown = cooldown;
        }
        else if (parent.tag == "Enemy")
        {
            weapon.Shoot();
            currentCooldown = cooldown;
        }
        cooldownBar.SetValue(currentCooldown);

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
            var barrelgfx = gameObject.transform.Find("BarrelGFX");
            if (barrelgfx) barrelgfx.GetComponent<SpriteRenderer>().sprite = weapon.weaponSprite;
        }
        else
        {
            Debug.LogError("Weapon subclass with the specified name not found.");
        }
    }
}
