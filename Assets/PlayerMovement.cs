using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;
    public float rotatSpeed = 0.5f;
    
    public float health;
    public float maxHealth = 100;

    public Rigidbody2D rb;
    Vector2 direccionMove;
    public HealthBar healthBar;

    void Update()
    {
        
        rb.rotation += -Input.GetAxisRaw("Horizontal") * (Input.GetAxisRaw("Vertical") != 0 ? rotatSpeed*0.7f:rotatSpeed);
        
        direccionMove = AssetHelper.DegreeToVector2(rb.rotation)* Input.GetAxisRaw("Vertical");


        //direccionMove.x = Input.GetAxisRaw("Horizontal");
        //direccionMove.y = Input.GetAxisRaw("Vertical");
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBar.SetHealth(health);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccionMove * speed * Time.fixedDeltaTime);
    }

}
