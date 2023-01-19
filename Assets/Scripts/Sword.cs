using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private CircleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    public Animator anim;
    
    private Vector3 _currentDirection;
    public Player player;
    public Vector2 difference;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myCollider2D = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        player._cooldownTimer = player.attackCooldown;
    }

    void Update()
    {
        if (player.moveInput != Vector3.zero)
        {
            _currentDirection = player.moveInput;
        }

        transform.position = player.transform.position + _currentDirection * 1.5f;
        
        Animate();

        if (player._cooldownTimer > 0)
        {
            player._cooldownTimer -= Time.deltaTime;

        }
        else if (player._attackDurationTimer > 0)
        {
            player._attackDurationTimer -= Time.deltaTime;
        }
        else
        {
            player._cooldownTimer = player.attackCooldown;
            player._attackDurationTimer = player.attackCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.ApplyDamage(player.damage);
                /*difference = enemy.transform.position - transform.position;
                difference = difference.normalized * player.knockBackPower;
                enemy.ReceiveKnockback(difference, player.knockBackTime, col);*/
            }
    } 
    void Animate()
    {
        anim.SetFloat("AnimMoveX",player.moveInput.x);
        anim.SetFloat("AnimMoveY",player.moveInput.y);
    }
}