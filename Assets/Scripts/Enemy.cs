using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D _myRigidbody;
    private Vector3 _movement;
    public Transform target;
    public float speed;
    public int health = 3;
    private int _currentHp;
    private void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _currentHp = health;
    }
    
    void FixedUpdate()
    {
       _movement = target.transform.position - transform.position;
       _myRigidbody.velocity = _movement.normalized * speed;
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
