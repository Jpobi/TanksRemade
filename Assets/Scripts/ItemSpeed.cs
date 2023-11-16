using System.Collections;
using UnityEngine;

public class ItemSpeed : Item
{
    protected override void ApplyTo(GameObject go)
    {
        EnemyAI enemyAI = go.GetComponentInChildren<EnemyAI>();
        PlayerMovement playerMovement = go.GetComponentInChildren<PlayerMovement>();

        if (enemyAI != null)
        {
            enemyAI.aiPath.maxSpeed += 2f;
            enemyAI.StartCoroutine(ReduceSpeed(enemyAI));
        }
        else if (playerMovement != null)
        {
            playerMovement.speed += 2f;
            playerMovement.StartCoroutine(ReduceSpeed(playerMovement));
        }
    }

    private IEnumerator ReduceSpeed(MonoBehaviour movement)
    {
        yield return new WaitForSeconds(3f);

        if (movement is EnemyAI enemyAI)
        {
            enemyAI.aiPath.maxSpeed -= 2f;
        }
        else if (movement is PlayerMovement playerMovement)
        {
            playerMovement.speed -= 2f;
        }
    }
}
