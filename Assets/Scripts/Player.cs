using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 moveInput;
    public float speed = 5f;
    public float swordSize = 3f;
    public LayerMask swordMask;
    public float attackCooldown = 1 ;
    public float cooldownTimer = 1 ;
    void Start()
    {
        
    }
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput.normalized * speed * Time.deltaTime;

        if (cooldownTimer >= 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = attackCooldown;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + moveInput, swordSize, moveInput, swordSize, swordMask);
        }
    }
}
