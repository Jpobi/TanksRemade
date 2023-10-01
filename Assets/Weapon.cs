using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon
{
    public int damage;
    public string weaponName;
    public Sprite weaponSprite;
    public Bullet bullet;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public abstract void Shoot(Transform fp);

}

public class BasicWeapon : Weapon
{
    public BasicWeapon() {
        this.damage = 10;
        this.weaponName = "Basic Cannon";
        //this.weaponSprite
        //this.bullet = new Bullet(
        //    AssetHelper.loadAsset("Assets/Tanks & Turrets - Free/Prefabs/projectiles/bomb_explosion.prefab"),
        //    AssetHelper.loadAsset("Assets/Tanks & Turrets - Free/Prefabs/projectiles/smoke.prefab"));
        //this.firePoint = firePoint;
        this.bulletPrefab = AssetHelper.loadPrefab("bullet-1");
        this.bulletForce = 10;
    }

    public override void Shoot(Transform fp)
    {
        this.firePoint=new GameObject("FirePointClone").transform;
        firePoint.position = fp.position;
        firePoint.rotation = fp.rotation;
        firePoint.Rotate(Vector3.forward, -90f);
        firePoint.position += firePoint.up.normalized * 1;
        GameObject bullet = UnityEngine.Object.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
