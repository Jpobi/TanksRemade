using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.5f;
    public float rotatSpeed = 3f;
    
    public float health;
    public float maxHealth = 100;
    //public int vidas = 3;
    public TextMeshProUGUI vidasCounter;

    public GameObject checkpoint;

    public Rigidbody2D rb;
    Vector2 direccionMove;
    public SliderBar healthBar;
    public string playerNum;

    //shield
    public GameObject shieldPrefab;
    public bool enableShield;
    public GameObject shieldIcon;


    //dash
    public SliderBar dashBar;
    public float cooldown = 6f; //cooldown de Dash
    public float currentCooldown;
    public AudioManager audio;

    void Update()
    {
        //shield
        if (enableShield)
        {
            shieldIcon.SetActive(true);
            if (Input.GetButtonDown("Fire2"))
            {
                GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
                shield.GetComponent<FollowFixedRotation>().target = gameObject;
                Destroy(shield, 10);
                enableShield = false;
            }
        }
        else
        {
            shieldIcon.SetActive(false);
        }

        
        //dashing
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        else if(Input.GetButtonDown("Jump"))
        {
            speed += 2f;
            StartCoroutine(ReduceSpeed());
            currentCooldown = cooldown;
        }
        dashBar.SetValue(currentCooldown);


        //tankthreads audio
        if (Input.GetAxisRaw("Vertical Left " + playerNum) == 0)
        {
            audio.Stop("TankMove");
        }
        else
        {
            Sound s = Array.Find(audio.sounds, item => item.name == "TankMove");
            if (s != null && !s.source.isPlaying)
            {
                audio.Play("TankMove");
            }
        }
        //direccionMove.x = Input.GetAxisRaw("Horizontal Left 1");
        //direccionMove.y = Input.GetAxisRaw("Vertical Left 1");
    }

    private void Start()
    {
        audio = FindAnyObjectByType<AudioManager>();
        //vidas = LevelManager.playerLives;
        health = maxHealth;
        healthBar.SetMaxValue(maxHealth);
        healthBar.SetValue(health);
        currentCooldown = cooldown;
        dashBar.SetMaxValue(cooldown);
        dashBar.SetValue(currentCooldown);
        vidasCounter.text = "Vidas: " + (LevelManager.playerLives+1);
    }
    public void TakeDamage(float damage)
    {
        this.health -= damage;
        healthBar.SetValue(health);
        if (this.health <= 0)
        {
            if (LevelManager.playerLives > 0)
            {
                LevelManager.playerLives -= 1;
                vidasCounter.text = "Vidas: " + (LevelManager.playerLives + 1);
                //LevelManager.playerLives = LevelManager.playerLives;
                this.transform.position= checkpoint.transform.position;
                health = maxHealth;
                healthBar.SetValue(health);
            }
            else
            {
                SceneManager.LoadScene("Lose");
            }
        }
    }

    void FixedUpdate()
    {
        if (playerNum != null && playerNum != "")
        {
            var actualRotatSpeed = (Input.GetAxisRaw("Vertical Left " + playerNum) != 0 ? rotatSpeed * 0.7f : rotatSpeed);
            rb.rotation += -Input.GetAxisRaw("Horizontal Left " + playerNum) * actualRotatSpeed;

            direccionMove = AssetHelper.DegreeToVector2(rb.rotation) * Input.GetAxisRaw("Vertical Left " + playerNum);
        }


        if (playerNum != null && playerNum!="")
        {
            rb.MovePosition(rb.position + direccionMove * speed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator ReduceSpeed()
    {
        yield return new WaitForSeconds(3f); //<-- duración dash

        speed -= 2f;
    }

}
