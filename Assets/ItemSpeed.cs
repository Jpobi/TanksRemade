using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : Item
{
    protected override void ApplyTo(GameObject go)
    {
        PlayerMovement playerMovement = go.GetComponentInChildren<PlayerMovement>();

        playerMovement.speed += 2f;

        playerMovement.StartCoroutine(ReduceSpeed(playerMovement));
    }

    private IEnumerator ReduceSpeed(PlayerMovement playerMovement)
    {
        yield return new WaitForSeconds(3f);

        playerMovement.speed -= 2f;
    }
}
