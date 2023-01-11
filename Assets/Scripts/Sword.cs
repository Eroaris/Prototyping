using UnityEngine;

public class Sword : MonoBehaviour
{
    private CircleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentDirection;
    public Player player;
    public LayerMask swordMask;
    public float attackCooldown = 1;
    private float _cooldownTimer = 1;
    private double _attackDuration = 0.3;
    public int damage = 3;
    void Awake()
    {
        _myCollider2D = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player.moveInput != Vector3.zero)
        {
            _currentDirection = player.moveInput;
        }
        transform.position = player.transform.position + _currentDirection * 0.7f;
        
        if (_cooldownTimer >= 0)
        {
            _attackDuration = 0.3;
            _cooldownTimer -= Time.deltaTime;
            _spriteRenderer.enabled = false;
        }
        else if (_attackDuration >= 0.3)
        {
            _cooldownTimer = attackCooldown;
            _attackDuration -= Time.deltaTime;
            _spriteRenderer.enabled = true;
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position , _myCollider2D.radius, swordMask);
            
            foreach (var collider2D in hits)
            {
                if (collider2D == _myCollider2D)
                {
                    continue;
                }
                Enemy enemy;
                enemy = collider2D.gameObject.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.ApplyDamage(damage);
                }
            }
        }
        
    }
}
