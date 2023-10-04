using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public string weaponName;
    public Sprite weaponSprite;
    public Bullet bullet;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;

    public abstract void Initialize(Transform firePoint);

    public abstract void Shoot();
}
public class BasicWeapon : Weapon
{
    public override void Initialize(Transform firePoint)
    {
        this.damage = 10;
        this.weaponName = "Basic Cannon";
        this.firePoint = firePoint;
        this.bulletPrefab = Resources.Load<GameObject>("bullet-2"); // Load prefab from Resources folder
        this.bulletForce = 10;
    }

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
public class Shotgun : Weapon
{
    public override void Initialize(Transform firePoint)
    {
        this.damage = 20;
        this.weaponName = "Basic Cannon";
        this.firePoint = firePoint;
        this.bulletPrefab = Resources.Load<GameObject>("bullet-2"); // Load prefab from Resources folder
        this.bulletForce = 10;
    }

    public override void Shoot()
    {
        Vector2 force = firePoint.up * bulletForce;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);

        bullet = Instantiate(bulletPrefab, firePoint.position + (firePoint.transform.right * 0.5f), firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Quaternion.Euler(0, 0, -45) * force, ForceMode2D.Impulse);

        bullet = Instantiate(bulletPrefab, firePoint.position - (firePoint.transform.right * 0.5f), firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Quaternion.Euler(0, 0, 45) * force, ForceMode2D.Impulse);

    }
}
