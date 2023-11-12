using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;
    public float rotatSpeed = 0.5f;
    
    public float health;
    public float maxHealth = 100;

    public Rigidbody2D rb;
    Vector2 direccionMove;
    public HealthBar healthBar;
    public string playerNum;

    void Update()
    {

        if (Input.GetButtonDown("Debug Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (playerNum != null && playerNum!="")
        {
            rb.rotation += -Input.GetAxisRaw("Horizontal Left " + playerNum) * (Input.GetAxisRaw("Vertical Left " + playerNum) != 0 ? rotatSpeed*0.7f:rotatSpeed);
        
            direccionMove = AssetHelper.DegreeToVector2(rb.rotation)* Input.GetAxisRaw("Vertical Left " + playerNum);
        }


        //direccionMove.x = Input.GetAxisRaw("Horizontal Left 1");
        //direccionMove.y = Input.GetAxisRaw("Vertical Left 1");
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
        if (playerNum != null && playerNum!="")
        {
            rb.MovePosition(rb.position + direccionMove * speed * Time.fixedDeltaTime);
        }
    }

}
