using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = target.transform.position;
        newPos.z=transform.position.z;
        gameObject.transform.SetPositionAndRotation(newPos,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = target.transform.position;
        newPos.z = transform.position.z;
        gameObject.transform.SetPositionAndRotation(newPos, Quaternion.identity);
    }
}
