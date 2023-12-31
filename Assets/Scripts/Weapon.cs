using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public int baseDamage;
    public string weaponName;
    public Sprite weaponSprite;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float cooldown;

    public abstract void Initialize(Transform firePoint);

    public abstract void Shoot();
}
public class BasicWeapon : Weapon
{
    public override void Initialize(Transform firePoint)
    {
        this.damage = 20;
        this.baseDamage = this.damage;
        this.weaponName = "Basic Cannon";
        this.weaponSprite = Resources.Load<Sprite>("basic_red");
        this.firePoint = firePoint;
        this.bulletPrefab = Resources.Load<GameObject>("bullet-1");
        this.bulletForce = 15;
        this.cooldown = 1.5f;
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
        this.damage = 15;
        this.baseDamage = this.damage;
        this.weaponName = "Shotgun";
        this.weaponSprite = Resources.Load<Sprite>("basic_black");
        this.firePoint = firePoint;
        this.bulletPrefab = Resources.Load<GameObject>("bullet-1");
        this.bulletForce = 10;
        this.cooldown = 3f;
    }

    public override void Shoot()
    {
        Vector2 force = firePoint.up * bulletForce;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);

        bullet = Instantiate(bulletPrefab, firePoint.position + (firePoint.transform.right * 0.4f), firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Quaternion.Euler(0, 0, -25) * force, ForceMode2D.Impulse);

        bullet = Instantiate(bulletPrefab, firePoint.position - (firePoint.transform.right * 0.4f), firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Quaternion.Euler(0, 0, 25) * force, ForceMode2D.Impulse);

    }
}

public class Sniper : Weapon
{
    public override void Initialize(Transform firePoint)
    {
        this.damage = 30;
        this.baseDamage = this.damage;
        this.weaponName = "Sniper";
        this.weaponSprite = Resources.Load<Sprite>("basic_blue");
        this.firePoint = firePoint;
        this.bulletPrefab = Resources.Load<GameObject>("bullet-1");
        this.bulletForce = 20;
        this.cooldown = 4f;
    }

    public override void Shoot()
    {
        Vector2 force = firePoint.up * bulletForce;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);

    }
}
