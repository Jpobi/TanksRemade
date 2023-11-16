using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EnemyAI : MonoBehaviour
{
    public GameObject barrel;
    public AIPath aiPath;

    public float visionRange=8.5f;
    public float health;
    public float maxHealth = 100;
    public float rotationSpeed = 2.0f;

    public LevelManager levelManager;

    //public GameObject target;
    public SliderBar healthBar;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
            var explosion = Resources.Load<GameObject>("bomb_explosion");
            GameObject effect = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            levelManager.enemyDeath();
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
