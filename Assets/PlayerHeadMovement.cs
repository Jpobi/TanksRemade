using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D body;
    public Camera cam;

    Vector2 mousePos;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetJoystickNames().ToString());
        mousePos=cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = body.position;
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
