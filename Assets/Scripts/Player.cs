using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
public class Player : MonoBehaviour
{
    public GameStateManager GSM;
    public Animator anim;
    public TextMeshProUGUI xptext;

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
                xptext.text = "XP needed:" + currentXP;
                break;
        }
    }
    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.LevelUP:
                currentXP = 0;
                maxXP += xpGrowth;
                canUpgrade = true;
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
        anim = GetComponent<Animator>();
        _currentHealth = maxHealth;
        canUpgrade = false;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput.normalized * speed * Time.deltaTime;
        Animate();

        xptext.text = currentXP+ "/"+ maxXP + "XP";
        if (currentXP >= maxXP)
        {
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

    public void UpgradeDamage()
    {
        if (canUpgrade)
        {
            damage++;
            print("Current Damage:"+damage);
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
     
     void OnTriggerStay(Collider other)
     {
         if (other.CompareTag("Goal"))
         {
             Goal goal = GetComponent<Goal>();
             goal.completeGoal();
         }
     }
     
     void Animate()
     {
         anim.SetFloat("AnimMoveX",moveInput.x);
         anim.SetFloat("AnimMoveY",moveInput.y);
     }
}
