using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public LevelManager levelManager;
    public int itemType;
    protected abstract void ApplyTo(GameObject go);


    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            ApplyTo(other.gameObject);
            levelManager.itemsList[itemType].cooldown = 20f;
            levelManager.itemsList[itemType].isPlaced = false;
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}