using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;

    private float rotatSpeed = 0.5f;

    public Rigidbody2D rb;
    Vector2 direccionMove;
    

    void Update()
    {
        
        rb.rotation += -Input.GetAxisRaw("Horizontal") * (Input.GetAxisRaw("Vertical") != 0 ? rotatSpeed*0.7f:rotatSpeed);
        direccionMove = AssetHelper.DegreeToVector2(rb.rotation)* Input.GetAxisRaw("Vertical");
        //direccionMove.x = Input.GetAxisRaw("Horizontal");
        //direccionMove.y = Input.GetAxisRaw("Vertical");
    }

    //private void Start()
    //{
    //}

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccionMove * speed * Time.fixedDeltaTime);
    }

}
