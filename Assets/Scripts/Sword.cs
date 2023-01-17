using UnityEngine;

public class Sword : MonoBehaviour
{
    private CircleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentDirection;
    public Player player;
    public LayerMask swordMask;

    void Awake()
    {
        _myCollider2D = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        player._cooldownTimer = player.attackCooldown;
    }

    void Update()
    {
        if (player.moveInput != Vector3.zero)
        {
            _currentDirection = player.moveInput;
        }

        transform.position = player.transform.position + _currentDirection * 1.5f;

        if (player._cooldownTimer > 0)
        {
            player._cooldownTimer -= Time.deltaTime;
            _spriteRenderer.enabled = false;
            
            
        }
        else if (player._attackDurationTimer > 0)
        {
            player._attackDurationTimer -= Time.deltaTime;
            _spriteRenderer.enabled = true;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _myCollider2D.radius, swordMask);

            if (hits.Length > 0)
            {
                foreach (var collider2D in hits)
                {
                    if (collider2D == _myCollider2D)
                    {
                        continue;
                    }

                    var enemy = collider2D.gameObject.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.ApplyDamage(player.damage);
                        Vector2 difference = enemy.transform.position - transform.position;
                        difference = difference.normalized * player.knockBackPower;
                        enemy.ReceiveKnockback(difference, player.knockBackTime);
                    }
                }
            }
           
        }
        else
        {
            _cooldownTimer = attackCooldown;
            _attackDurationTimer = _attackDuration;
        }
    }
    
}