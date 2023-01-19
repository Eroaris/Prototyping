using UnityEngine;
using UnityEngine.Serialization;
public class Player : MonoBehaviour
{
    private Sword sword;
    public GameStateManager GSM;
    private GameStateManager.GameState _gameState;
    public Animator anim;
   
    public Vector3 moveInput;
    public bool canUpgrade;
    public float speed = 4.5f;
    public int maxHealth = 3;
    private int _currentHealth;
    private float _lastHitTime;
    private const float IFrameDuration = 2;
    private int currentXP;
    public float maxXP = 10;
    public float knockBackPower = 1;
    public float knockBackTime = 0.5f;
    public float attackCooldown = 0.8f;
    public float _cooldownTimer;
    public float _attackDurationTimer = 0.25f;
    public float _attackDuration = 0.25f;
    public int damage;
    
    
    private void OnEnable()
    {
        GameStateManager.OnGameStateChanged += OnGameStateChanged;
        Enemy.OnEnemyDestroyed -= WhenEnemyDestroyed;
    }
    private void OnDisable()
    {
        GameStateManager.OnGameStateChanged -= OnGameStateChanged;
        Enemy.OnEnemyDestroyed += WhenEnemyDestroyed;
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
            case GameStateManager.GameState.Ready:
                canUpgrade = false;
                break;
            
            case GameStateManager.GameState.LevelUP:
                
                break;
            
            case GameStateManager.GameState.Win:
                //cheer animation+vitory screen
                break;

            case GameStateManager.GameState.Lose:
                //death animation+game over
                break;
        }
    }

    void Awake()
    {
        sword = GetComponentInChildren<Sword>();
        anim = GetComponent<Animator>();
        _currentHealth = maxHealth;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput.normalized * speed * Time.deltaTime;
        
        Animate();
        
        _gameState = GSM.GetCurrentState();
        if (currentXP >= maxXP)
        {
            print(currentXP);
            currentXP = 0;
            maxXP *= 0.80f;
            canUpgrade = true;
            GSM.SetCurrentState(GameStateManager.GameState.LevelUP);
        }
    }
    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _currentHealth -= damageAmount;
            _lastHitTime = Time.realtimeSinceStartup;
            print("Damage Taken! HP left:"+_currentHealth);
        }

        if (_currentHealth <= 0)
        {
            print("You Lose");
            Debug.Break();
            GSM.SetCurrentState(GameStateManager.GameState.Lose);
        }

        return _currentHealth;
    }
    
     public void UpgradeHealth()
     {
         if (canUpgrade)
         {
             _currentHealth += 1;
             maxHealth += 1;
             print("Max Health:"+ maxHealth);
             print("Current Health:" + _currentHealth);
             GSM.SetCurrentState(GameStateManager.GameState.Ready);
         }
     }
     public void UpgradeSpeed()
     {
         if (canUpgrade)
         {
             speed += 0.25f;
             print("Movespeed:" + speed);
             GSM.SetCurrentState(GameStateManager.GameState.Ready);
         }
     }
     void Animate()
     {
         anim.SetFloat("AnimMoveX",moveInput.x);
         anim.SetFloat("AnimMoveY",moveInput.y);
     }
}
