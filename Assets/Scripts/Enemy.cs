using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Vector3 _movement;
    public Transform target;
    public float speed;
    private float maxSpeed;
    private Vector2 maxVelocity;
    public int health = 3;
    private int _currentHp;
    private float _lastHitTime;
    private const float IFrameDuration = 1;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        _currentHp = health;
        maxSpeed = speed + 1;
    }

    void Update()
    {
       _movement = target.transform.position - transform.position;
       myRigidbody.velocity += (Vector2) _movement.normalized * speed * Time.deltaTime;
       print(myRigidbody.velocity.magnitude);
       if (myRigidbody.velocity.magnitude > maxSpeed)
       {
           myRigidbody.velocity = Vector2.ClampMagnitude(myRigidbody.velocity, maxSpeed);
       }
    }
    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _currentHp -= damageAmount;
            _lastHitTime = Time.realtimeSinceStartup;
        }

        if (_currentHp <= 0)
        {
            Destroy(gameObject);
        }

        return _currentHp;
    }
}
