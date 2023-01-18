using UnityEngine;

public class Player : MonoBehaviour
{
    private Sword sword;
    public GameStateManager GSM;
    public Vector3 moveInput;
    public float speed = 4.5f;
    public int health = 3;
    private int _currentHp;
    private float _lastHitTime;
    private const float IFrameDuration = 2;
    private int currentXP;
    public float maxXP = 10;
    public float knockBackPower = 1;
    public float knockBackTime = 0.5f;
    public float attackCooldown = 0.8f;
    public float _cooldownTimer;
    public float _attackDurationTimer = 0.25f;
    private float _attackDuration = 0.25f;
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

    public void WhenEnemyDestroyed(Enemy enemy)
    {
        currentXP++;
        print(currentXP);
    }

    private void OnGameStateChanged(GameStateManager.GameState targetstate)
    {
        switch (targetstate)
        {
            case GameStateManager.GameState.LevelUP:
                maxXP *= 0.2f;
                currentXP = 0;
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
        _currentHp = health;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput.normalized * speed * Time.deltaTime;
        if (currentXP >= maxXP)
        {
            GSM.SetCurrentState(GameStateManager.GameState.LevelUP);
        }
    }

    private void LevelUp()
    {
            print("Level Up!");
            int randomUpgrade = Random.Range(0,4);

            switch (randomUpgrade)
            {
                case 0:
                    health++;
                    _currentHp++;
                    break;
                case 1:
                    damage++;
                    break;
                case 2:
                    speed += 0.25f;
                    break;
                case 3:
                    knockBackPower += 0.5f;
                    break;
            }

            
    }

    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _currentHp -= damageAmount;
            _lastHitTime = Time.realtimeSinceStartup;
            print("Damage Taken!");
        }

        if (_currentHp <= 0)
        {
            print("You Lose");
            Debug.Break();
            GSM.SetCurrentState(GameStateManager.GameState.Lose);
        }

        return _currentHp;
    }
    
     private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        enemy.ReceiveKnockback(sword.difference, knockBackTime);
    }
}
