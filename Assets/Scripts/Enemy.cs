using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    public delegate void EnemyDestroyedDelegate(EnemyState enemyState);
    public static event EnemyDestroyedDelegate OnEnemyDestroyed;
    
    private SpriteRenderer _spriteRenderer; 
    public Rigidbody2D myRigidbody;
    public Vector3 _movement;
    public Transform target;
    public Animator anim;
    private AudioSource audioSource;

    public float speed;
    public float maxSpeed;
    private Vector2 maxVelocity;
    
    public int baseHealth = 3;
    private float healthIncrease;
    private float maxHP;
    private float _currentHp;
   
    private float _lastHitTime;
    private const float IFrameDuration = 0.5f;
   
    private bool isDying;
    private bool inKnockback;
    public float spawnOffset = 0.2f;
    public AudioClip onHit;
    
    public enum EnemyState
    {
        Alive,
        Dead
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isDying = false;
        healthIncrease = Time.time / 30;
        healthIncrease = Mathf.Floor(healthIncrease);
        maxHP = healthIncrease + baseHealth;
    }
    private void Start()
    {
        _currentHp = maxHP;
        OnEnemyDestroyed?.Invoke(EnemyState.Alive);
        inKnockback = false;
    } 
    void FixedUpdate()
    {
        _movement = target.transform.position - transform.position;
        if (isDying == false)
        {
            myRigidbody.velocity += (Vector2) _movement.normalized * speed;
        }
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
    public void ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup && isDying == false)
        {
            _currentHp -= damageAmount;
            audioSource.PlayOneShot(onHit);
            StartCoroutine(FlashOnDamage(IFrameDuration));
            _lastHitTime = Time.realtimeSinceStartup;
        }

        if (_currentHp <= 0)
        {
            Death();
            
        }
    }
    private IEnumerator FlashOnDamage(float FlashDuration)
    {
        _spriteRenderer.color = new Color(0.8235294f, 0.375534f,0.375534f,1);
        
        yield return new WaitForSeconds(FlashDuration);
        
        _spriteRenderer.color = Color.white;
    }
    public void ReceiveKnockback(Vector2 difference,float knockbackTime, Collider2D col)
    {
        if (col != null )
        {
            if (inKnockback == false)
            {
                inKnockback = true;
                myRigidbody.AddForce(difference, ForceMode2D.Impulse);
                StartCoroutine(KnockbackCo(knockbackTime));
            }
        }
    }
    private IEnumerator KnockbackCo(float knockBackTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        myRigidbody.velocity = Vector2.zero;
        inKnockback = false;
    }  
   void DestroySelf()
    {
        print("Enemy Killed");
        Destroy(gameObject);
        OnEnemyDestroyed?.Invoke(EnemyState.Dead);
    }

   void Death()
   {
       anim.SetTrigger("Death");
       isDying = true;
       Invoke(nameof(DestroySelf),0.5f);
   }
}
