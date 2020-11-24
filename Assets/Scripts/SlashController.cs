﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private GameManager gm;
    public PlayerBehavior player;
    public SpriteRenderer sprite;



    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameManager>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        //Quaternion rotation = Quaternion.Euler(-1, 0, -90);
        //transform.rotation = rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        float vel = player.GetComponent<SpriteRenderer>().flipX == true
            ? -speed
            : speed;
        if(player.GetComponent<SpriteRenderer>().flipX == true)
        {
            sprite.flipX = true;
        }
        rb.velocity = new Vector2(vel, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") )
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Debug.Log("i hit En");
        }

        if (collision.CompareTag("MG"))
        {
            Destroy(gameObject);
            Debug.Log("i hit MG");
        }
    }

    public void setDirection(bool dir)
    {
        sprite.flipX = dir;
            
    }
}