using System;
using UnityEngine;

public class ExplodyEnemy : MonoBehaviour
{
    private Enemy _enemy;
    private Vector3 distanceToPlayer;
    public LayerMask explodyMask;
    public int damage = 3;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        
    }

    private void Update()
    {
        float dist = Vector3.Distance(_enemy.target.transform.position, transform.position);

        if (dist <= 3)
        {
            float explosionTimer = 3;
            explosionTimer -= Time.deltaTime;
            if (explosionTimer <= 0)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2f, explodyMask);

                foreach (var collider2D in hits)
                {
                    Player player;
                    player = collider2D.gameObject.GetComponent<Player>();

                    if (player != null)
                    {
                        player.ApplyDamage(damage);
                    }
                    
                    Enemy enemy;
                    enemy = collider2D.gameObject.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.ApplyDamage(damage);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
