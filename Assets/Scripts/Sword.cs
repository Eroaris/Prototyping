using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private CapsuleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator anim;
    
    private Vector3 _currentDirection;
    public Player player;
    public Vector2 difference;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        player._cooldownTimer = player.attackCooldown;
    }

    void Update()
    {
        if (player.moveInput != Vector3.zero)
        {
            _currentDirection = player.moveInput;
        }
        
        _myCollider2D.enabled = !_myCollider2D.enabled;
        Animate();

        
        /*if (player._cooldownTimer > 0)
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
        }*/
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                enemy.ApplyDamage(player.damage);
                difference = enemy.transform.position - player.transform.position;
                difference = difference.normalized * player.knockBackPower;
                enemy.ReceiveKnockback(difference, player.knockBackTime, col);
            }
    } 
    void Animate()
    {
        anim.SetFloat("AnimMoveX",_currentDirection.x);
        anim.SetFloat("AnimMoveY",_currentDirection.y);
    }
}