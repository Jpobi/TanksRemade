using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;

public class EnemyAI : MonoBehaviour
{
    public GameObject barrel;
    public AIPath aiPath;

    public float visionRange=8.5f;
    public float health;
    public float maxHealth = 100;
    public float rotationSpeed = 2.0f;

    public SliderBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxValue(maxHealth);
    }

    void Update()
    {
        Aim();

    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBar.SetValue(health);
        if (this.health <= 0)
        {
            AssetHelper.ShowText(transform.position, Color.green, 75, "GANASTE!");
            this.gameObject.SetActive(false);
        }
    }

    public void Aim()
    {
        barrel.transform.position = transform.position;

        var desiredVelocity = aiPath.desiredVelocity;
        var toTarget = aiPath.target.position - transform.position;

        Quaternion targetRotation;

        if (toTarget.magnitude < visionRange)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, toTarget.normalized);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, desiredVelocity);
        }

        barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
