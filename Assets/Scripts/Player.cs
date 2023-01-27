using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameStateManager GSM;
    public GameObject levelUPUI;
    public Animator anim;
    public Animator swordAnim;
    private SpriteRenderer _spriteRenderer;
    public Canvas gameOverScreen;

    private AudioSource audioSource;
    public AudioClip lvlUp;
    public AudioClip dmgTaken;
    public AudioClip xpGained;
    
    public Vector3 moveInput;
    public bool canUpgrade;
    public float speed = 4.5f;
    
    public Image[] hearts;
    public int _currentHealth;

    private float _lastHitTime;
    private const float IFrameDuration = 2;
   
    public int currentXP;
    public float maxXP = 10;
    
    public float knockBackPower = 1;
    public float knockBackTime;
    
    public float attackCooldown;
    public float _cooldownTimer = 0.5f;
    private bool isInCooldown;
    
    public int damage;
    public float swordRange;
    
    public float xpGrowth = 3;
    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
        Enemy.OnEnemyDestroyed += WhenEnemyDestroyed;
    }
    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
        Enemy.OnEnemyDestroyed -= WhenEnemyDestroyed;
    }

    public void WhenEnemyDestroyed(Enemy.EnemyState enemyState)
    {
        switch (enemyState)
        {
            case Enemy.EnemyState.Dead:
                if (anim.GetBool("Dying") == false)
                {
                    currentXP++;
                    audioSource.PlayOneShot(xpGained,0.5f);
                } 
                break;
        }
    }
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.Playing:
                levelUPUI.SetActive(false);
                break;
            
            case GameStateManager.GameState.LevelUP:
                currentXP = 0;
                maxXP += xpGrowth;
                canUpgrade = true;
                levelUPUI.SetActive(true);
                break;
            
            case GameStateManager.GameState.Win:
                //cheer animation+vitory screen
                break;

            case GameStateManager.GameState.Lose:
                /*Time.timeScale = 0;
                anim.updateMode = AnimatorUpdateMode.UnscaledTime;*/
                removeBlue();
                anim.SetBool("Dying",true);
                Invoke(nameof(GameOver),3);
                break;
        }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        anim.updateMode = AnimatorUpdateMode.Normal;
        _cooldownTimer = attackCooldown;
        canUpgrade = false;
        swordAnim.speed = 0.75f;
        swordRange = 1;

        for (int i = _currentHealth; i >= 0; i--)
        {
            hearts[i].gameObject.SetActive(true);
        }   
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (anim.GetBool("Dying") == false)
        {
            transform.position += moveInput.normalized * speed * Time.deltaTime;
        }
        
        Animate();
        
        if (currentXP >= maxXP && anim.GetBool("Dying") == false)
        {
            GSM.SetCurrentState(GameStateManager.GameState.LevelUP);
            audioSource.PlayOneShot(lvlUp,0.7f);
        }

        isInCooldown = _cooldownTimer > 0;
        if (isInCooldown)
        {
            _cooldownTimer -= Time.deltaTime;   
        }
       
        
        swordAnim.SetBool("InCooldown",isInCooldown);

       if (GSM.GetCurrentState() == GameStateManager.GameState.Lose)
       {
           if (Input.GetKeyDown(KeyCode.Space))
           {
               SceneManager.LoadScene("SampleScene");
           }
       }
    }

    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup && anim.GetBool("Dying") == false)
        {
            hearts[_currentHealth].gameObject.SetActive(false);
            _currentHealth--;
            _spriteRenderer.color = Color.cyan;
            Invoke(nameof(removeBlue),IFrameDuration);
            audioSource.PlayOneShot(dmgTaken,0.7f);
            _lastHitTime = Time.realtimeSinceStartup;
        }

        if (_currentHealth < 0)
        {
            GSM.SetCurrentState(GameStateManager.GameState.Lose);
        }

        return _currentHealth;
    }

    void removeBlue()
    {
        _spriteRenderer.color = Color.white;
    }
    public void UpgradeDamage()
    {
        if (canUpgrade)
        {
            damage++;
            canUpgrade = false;
            GSM.SetCurrentState(GameStateManager.GameState.Playing);
        }
    }
    
    public void UpgradeHealth()
     {
         if (canUpgrade)
         {
             _currentHealth++;
             hearts[_currentHealth].gameObject.SetActive(true);
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }
     public void UpgradeSpeed()
     {
         if (canUpgrade)
         {
             speed += 0.25f;
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }

     public void UpgradeAttackSpeed()
     {
         if (canUpgrade)
         {
             attackCooldown *= 0.85f;
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }

     public void UpgradeKnockback()
     {
         if (canUpgrade)
         {
            knockBackPower += 3;
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }
     
     void Animate()
     {
         anim.SetFloat("AnimMoveX",moveInput.x);
         anim.SetFloat("AnimMoveY",moveInput.y);
     }

     public void GameOver()
     {
         gameOverScreen.gameObject.SetActive(true);
         Time.timeScale = 0;
     }
}
