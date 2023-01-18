using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy Instance;
    public delegate void EnemyDestroyedDelegate(Enemy enemy);
    public static event EnemyDestroyedDelegate OnEnemyDestroyed;
    private SpriteRenderer _spriteRenderer; 
    public Rigidbody2D myRigidbody;
    public Vector3 _movement;
    public Transform target;
    public float speed;
    public float maxSpeed;
    private Vector2 maxVelocity;
    public int health = 3;
    private int _currentHp;
    private float _lastHitTime;
    private const float IFrameDuration = 0.5f;
    private bool inKnockback;
    public float spawnOffset = 0.2f;
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
            
            case GameStateManager.GameState.LevelUP:
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Start()
    {
        _currentHp = health;
    } 
    void FixedUpdate()
    {
       _movement = target.transform.position - transform.position;
       myRigidbody.velocity += (Vector2) _movement.normalized * speed;
       if (myRigidbody.velocity.magnitude > maxSpeed && inKnockback == false)
       {
           myRigidbody.velocity = Vector2.ClampMagnitude(myRigidbody.velocity, maxSpeed);
       }

       if (transform.position.x <= -47)
       {
           transform.position = Vector3.left * spawnOffset;
       }
       if (transform.position.x >= 47)
       {
           transform.position = Vector3.right * spawnOffset;
       }
       if (transform.position.y <= -26)
       {
           transform.position = Vector3.up * spawnOffset;
       }
       if (transform.position.y >= 26)
       {
           transform.position = Vector3.down * spawnOffset;
       }
    }
    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _spriteRenderer.color = Color.yellow;
            _currentHp -= damageAmount;
            _lastHitTime = Time.realtimeSinceStartup;
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
        
        if (_currentHp <= 0)
        {
            DestroySelf();
            print("Enemy Killed");
        }

        return _currentHp;
    }
    

    public void ReceiveKnockback(Vector2 difference,float knockbackTime)
    {
        inKnockback = true;
        myRigidbody.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockbackCo(knockbackTime));
        
    }
    private IEnumerator KnockbackCo(float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        
        myRigidbody.velocity = Vector2.zero;
        inKnockback = false;
    }  
    public void DestroySelf()
    {
        OnEnemyDestroyed?.Invoke(this);
        print("Enemy Killed");
        Destroy(gameObject);
    }

}
