using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    public GameObject smoke;
    public float damage;
    UnityEngine.Camera cam;
    public GameObject text;

    public Bullet(GameObject explosion, GameObject smoke)
    {
        this.explosion = explosion;
        this.smoke = smoke;
    }

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (gameObject.activeSelf)
        {
            //rotar bala hacia su direccion
            Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;

            if (velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = targetRotation;
            }
            Vector3 newPos = transform.position - (transform.right.normalized * this.GetComponent<SpriteRenderer>().size.x * 1.2f);
            //TODO: FIX TRAIL
            GameObject trail = Instantiate(smoke,newPos, Quaternion.identity);
            trail.GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(trail, 0.2f);
        }


        if (!IsObjectInCameraBounds(gameObject)){
            Destroy(gameObject);
        }
    }
    bool IsObjectInCameraBounds(GameObject obj)
    {
        Vector2 viewportPosition = cam.WorldToViewportPoint(obj.transform.position);
        return viewportPosition.x >= 0f && viewportPosition.x <= 1f &&
               viewportPosition.y >= 0f && viewportPosition.y <= 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shield"))
        {
            //shield with damage that withstands X number of hits(?)  //Normal Shield
            //collision.collider.GetComponent<PlayerMovement>().TakeDamage(damage);
            //BouncyShield
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            gameObject.GetComponent<Rigidbody2D>().velocity = ((Vector2)gameObject.transform.position - (Vector2)collision.gameObject.transform.position) *gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            //solo para debug TODO: ELIMIANR
            //transform.forward = -transform.forward;
        }
        else
        {
            GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            //GameObject texto = Instantiate(text, transform.position, Quaternion.identity);
            //texto.GetComponent<TextMesh>().color = Color.red;
            //texto.GetComponent<TextMesh>().text = damage.ToString();
            //Destroy(texto, 1f);
            AssetHelper.ShowText(transform.position+new Vector3(0,1.5f,0), Color.red,40, damage.ToString());
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Deformable"))
        {
            DeformableCollider deformableCollider = collision.collider.GetComponent<DeformableCollider>();
            if (deformableCollider != null)
            {
                // Get the impact point and deform the collider
                Vector2 impactPoint = collision.GetContact(0).point;
                deformableCollider.Deform(impactPoint, transform.up, 0.6f);
            }
        }
        else if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerMovement>().TakeDamage(damage);
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyAI>().TakeDamage(damage);
        }
    }
}
