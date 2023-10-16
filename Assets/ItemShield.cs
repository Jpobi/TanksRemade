using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemShield : Item
{
    public GameObject shieldPrefab;
    protected override void ApplyTo(GameObject go)
    {
        PlayerMovement playerMovement = go.GetComponentInChildren<PlayerMovement>();
        GameObject shield = Instantiate(shieldPrefab, go.transform.position,Quaternion.identity);
        shield.GetComponent<FollowFixedRotation>().target=go;
        Destroy(shield, 3);
    }
}
