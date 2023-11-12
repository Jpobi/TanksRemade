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
    string playerNum;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = body.gameObject.GetComponent<PlayerMovement>().playerNum;
        print("controles: ");
        int i = 0;
        foreach (string nom in Input.GetJoystickNames())
        {
            if (nom != "") { print(i.ToString()+" : "+nom.ToString()); } else { print(i); };
            i++;
        };
    }

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
        //Keyboard&Mouse
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        //Controller
        if (playerNum != null && playerNum!="")
        {
            Vector2 newAim = new Vector2(Input.GetAxisRaw("Horizontal Right " + playerNum), Input.GetAxisRaw("Vertical Right " + playerNum));
            if (newAim != Vector2.zero)
            {
                aim = newAim;
                //print("Horizontal R: " + newAim.x);
                //print("vertical R" + newAim.y);
            }
            //float angle = Mathf.Atan2(newAim.y, newAim.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
