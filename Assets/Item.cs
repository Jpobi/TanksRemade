using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void ApplyTo(GameObject go);

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyTo(other.gameObject);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}