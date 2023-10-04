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
    public Text text;
    public Bullet(GameObject explosion, GameObject smoke/*, Camera cam*/)
    {
        this.explosion = explosion;
        this.smoke = smoke;
        //this.cam = cam;
    }

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;

            if (velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = targetRotation;
            }
            Vector3 newPos = transform.position - (transform.right.normalized * this.GetComponent<SpriteRenderer>().size.x * 1.2f);
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
        GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Text texto = Instantiate(text, transform.position, Quaternion.identity);
        texto.color = Color.red;
        texto.text=damage.ToString();
        Destroy(texto, 1f);
        Destroy(gameObject);

        if (collision.collider.CompareTag("Deformable"))
        {
            DeformableCollider deformableCollider = collision.collider.GetComponent<DeformableCollider>();
            if (deformableCollider != null)
            {
                // Get the impact point and deform the collider
                Vector2 impactPoint = collision.GetContact(0).point;
                deformableCollider.Deform(impactPoint,transform.up, 0.6f);
            }
        }
    }
}
