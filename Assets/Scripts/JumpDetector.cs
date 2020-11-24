﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetector : MonoBehaviour
{

    private PlayerBehavior player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTERED TRIGGER JUMP RESET");
        if(!collision.CompareTag("Player"))
        {

            player.resetJump();
        }
    }
}
