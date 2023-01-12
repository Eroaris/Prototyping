using System;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    private Enemy _enemy;
    public int damage = 2;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player;
        player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
