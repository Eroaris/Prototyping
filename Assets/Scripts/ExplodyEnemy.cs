
using System;
using UnityEngine;


public class ExplodyEnemy : MonoBehaviour
{
    private Enemy _enemy;
    public LayerMask explodyMask;
    public float explosionTimer = 3;
    public int playerDamage = 3;
    private readonly int _enemyDamage = 999;
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(_enemy.target.transform.position, transform.position);
        
        if (dist <= 3)
        {
            explosionTimer -= Time.deltaTime;
            if (explosionTimer <= 0)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f, explodyMask);

                foreach (var collider2D in hits)
                {
                    var player = collider2D.gameObject.GetComponent<Player>();
                    
                    if (player != null)
                    {
                        player.ApplyDamage(playerDamage);
                        Destroy(gameObject);
                    }

                    var enemy = collider2D.gameObject.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        enemy.ApplyDamage(_enemyDamage);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
          var player =  col.gameObject.GetComponent<Player>();
          player.ApplyDamage(playerDamage);
          Destroy(gameObject);
        }
    }
}
