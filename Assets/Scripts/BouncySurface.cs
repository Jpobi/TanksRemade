using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;

        if (collision.contacts.Length > 0)
        {
            Vector2 contactNormal = collision.contacts[0].normal;
            Vector2 bounceDirection = Vector2.Reflect(collision.relativeVelocity.normalized, contactNormal);

            rb.AddForce(bounceDirection * collision.relativeVelocity.magnitude, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
