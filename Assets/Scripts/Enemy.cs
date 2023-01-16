using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy Instance;
    public delegate void EnemyDestroyedDelegate(Enemy enemy);
    public static event EnemyDestroyedDelegate OnEnemyDestroyed;
    
    public Rigidbody2D myRigidbody;
    public Vector3 _movement;
    public Transform target;
    public float speed;
    private float maxSpeed;
    private Vector2 maxVelocity;
    public int health = 3;
    private int _currentHp;
    private float _lastHitTime;
    private const float IFrameDuration = 0.5f;
    
    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.Ready:
                break;
            
            case GameStateManager.GameState.Playing:
                break;
            
            case GameStateManager.GameState.Upgrade:
                break;
                
            case GameStateManager.GameState.Pause:
                break;
            
            case GameStateManager.GameState.Win:
                break;
            
            case GameStateManager.GameState.Lose:
                break;
            
        }
    }
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
            DestroySelf();
        }

        return _currentHp;
    }

    public void ReceiveKnockback(Vector2 difference,float knockbackTime)
    {
        myRigidbody.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCo(knockbackTime));
        
    }
    private IEnumerator KnockbackCo(float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        
        myRigidbody.velocity = Vector2.zero;
    }  
    public void DestroySelf()
    {
        Destroy(gameObject);
        OnEnemyDestroyed.Invoke(this);
        
    }
    
    
}
