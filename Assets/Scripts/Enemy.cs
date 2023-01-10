using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider2D _myCollider2D;
    private Rigidbody2D _myRigidbody;
    private Vector3 _movement;
    public Transform target;
    public float speed = 2f;
    private int _damage = 3;
    public int health = 3;
    private int _currentHp;
    private void Awake()
    {
        _myCollider2D = GetComponent<Collider2D>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _currentHp = health;
    }
    
    void FixedUpdate()
    {
       _movement = target.transform.position - transform.position;
       _myRigidbody.velocity = _movement.normalized * speed;
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player;
        player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ApplyDamage(_damage);
        }
    }

    public int ApplyDamage(int damageAmount)
    {
        _currentHp -= damageAmount;
       
            if (_currentHp <= 0)
        {
            Destroy(gameObject);
        }

        return _currentHp;
    }
}
