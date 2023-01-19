using UnityEngine;

public class oldsword : MonoBehaviour
{
   private CircleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _currentDirection;
    public Player player;
    public Vector2 difference;
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
        }
        else if (player._attackDurationTimer > 0)
        {
            player._attackDurationTimer  -= Time.deltaTime;
        }
        else
        {
            player._cooldownTimer = player.attackCooldown;
            player._attackDurationTimer = player._attackDuration;
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
}

