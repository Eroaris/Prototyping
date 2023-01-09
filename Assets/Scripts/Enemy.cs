using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float speed = 2f;
    private Vector3 movement;
    private Rigidbody2D myRigidbody;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
       movement = player.transform.position - transform.position;
       myRigidbody.velocity = movement * speed ;
    }
}
