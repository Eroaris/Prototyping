using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
public class Player : MonoBehaviour
{
    public GameStateManager GSM;
    public GameObject levelUPUI;
    public Animator anim;
    public Animator swordAnim;

    public Vector3 moveInput;
    public bool canUpgrade;
    public float speed = 4.5f;
    
    public int maxHealth = 3;
    private int _currentHealth;
    public TextMeshProUGUI healthtext;
    
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
                currentXP++;
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
                anim.SetBool("Dying",true);
                break;
        }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        _currentHealth = maxHealth;
        _cooldownTimer = attackCooldown;
        canUpgrade = false;
        swordAnim.speed = 0.75f;
        healthtext.text = _currentHealth + "/" + maxHealth;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput.normalized * speed * Time.deltaTime;
        Animate();
        
        if (currentXP >= maxXP)
        {
            GSM.SetCurrentState(GameStateManager.GameState.LevelUP);
        }

        isInCooldown = _cooldownTimer > 0;
        if (isInCooldown)
        {
            _cooldownTimer -= Time.deltaTime;   
        }
       swordAnim.SetBool("InCooldown",isInCooldown);
    }

    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _currentHealth -= damageAmount;
            _lastHitTime = Time.realtimeSinceStartup;
            healthtext.text = _currentHealth + "/" + maxHealth;
        }

        if (_currentHealth <= 0)
        {
            print("You Lose");
            Debug.Break();
            GSM.SetCurrentState(GameStateManager.GameState.Lose);
        }

        return _currentHealth;
    }

    public void UpgradeDamage()
    {
        if (canUpgrade)
        {
            damage++;
            print("Current Damage:"+damage);
            print(Time.timeScale);
            canUpgrade = false;
            GSM.SetCurrentState(GameStateManager.GameState.Playing);
        }
    }
    
    public void UpgradeHealth()
     {
         if (canUpgrade)
         {
             _currentHealth++;
             maxHealth++;
             print("Max Health:"+ maxHealth);
             print("Current Health:" + _currentHealth);
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }
     public void UpgradeSpeed()
     {
         if (canUpgrade)
         {
             speed += 0.25f;
             print("Movespeed:" + speed);
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }

     public void UpgradeAttackSpeed()
     {
         if (canUpgrade)
         {
             attackCooldown *= 0.85f;
             print(attackCooldown);
             canUpgrade = false;
             GSM.SetCurrentState(GameStateManager.GameState.Playing);
         }
     }
     void Animate()
     {
         anim.SetFloat("AnimMoveX",moveInput.x);
         anim.SetFloat("AnimMoveY",moveInput.y);
     }
}
