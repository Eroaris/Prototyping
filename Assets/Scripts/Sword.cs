using UnityEngine;

public class Sword : MonoBehaviour
{
    private CircleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentDirection;
    public Player player;
    public LayerMask swordMask;
    public float attackCooldown = 2;
    private float _cooldownTimer = 2;
    private float _attackDurationTimer = 0.25f;
    private float _attackDuration = 0.25f;
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
        
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            _spriteRenderer.enabled = false;
        }
        else if (_attackDurationTimer > 0)
        {
            print(true);
            _attackDurationTimer -= Time.deltaTime;
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
        else
        {
            print("reset");
            _cooldownTimer = attackCooldown;
            _attackDurationTimer = _attackDuration;
        }  
    }
}
