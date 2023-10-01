using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 2.5f;
    private float rotatSpeed = 0.5f;

    public Rigidbody2D rb;
    Vector2 direccionMove;
    public Weapon initialWeapon;
    

    void Update()
    {
        
        rb.rotation += -Input.GetAxisRaw("Horizontal") * (Input.GetAxisRaw("Vertical") != 0 ? rotatSpeed*0.7f:rotatSpeed);
        direccionMove = AssetHelper.DegreeToVector2(rb.rotation)* Input.GetAxisRaw("Vertical");
        //direccionMove.x = Input.GetAxisRaw("Horizontal");
        //direccionMove.y = Input.GetAxisRaw("Vertical");
    }

    private void Start()
    {
        initialWeapon = new BasicWeapon();
        Debug.Log(initialWeapon.weaponName);
        gameObject.GetComponentInChildren<Shooting>().weapon = initialWeapon;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direccionMove * speed * Time.fixedDeltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.layer==1) {
        
    //        gameObject.GetComponent<Shooting>.weapon=collision.gameObject;
    //        collision.gameObject.SetActive(false);

    //    }
    //}
}
