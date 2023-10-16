using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerHeadMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D body;
    public Camera cam;
    Vector2 aim;
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

        Vector2 newAim = new Vector2(Input.GetAxisRaw("Horizontal R"), Input.GetAxisRaw("Vertical R"));
        if (newAim != aim)
        {
            print("Horizontal R: " + newAim.x);
            print("vertical R" + newAim.y);
        }
        aim = newAim;
        float angle = Mathf.Atan2(newAim.y, newAim.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
