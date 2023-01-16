using UnityEngine;

public class Player : MonoBehaviour
{
    public GameStateManager GSM;
    public Vector3 moveInput;
    public float speed = 5f;
    public int health = 3;
    private int _currentHp;
    private float _lastHitTime;
    private const float IFrameDuration = 2;
    
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
            case GameStateManager.GameState.Start:
                break;
            
            case GameStateManager.GameState.Ready:
                break;
            
            case GameStateManager.GameState.Playing:
                break;
            
            case GameStateManager.GameState.Upgrade:
                break;
                
            case GameStateManager.GameState.Pause:
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
        _currentHp = health;
    }
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput.normalized * speed * Time.deltaTime;
    }
    public int ApplyDamage(int damageAmount)
    {
        float check = _lastHitTime + IFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _currentHp -= damageAmount;
            _lastHitTime = Time.realtimeSinceStartup;
            print(_currentHp);
        }
        
        if (_currentHp <= 0)
        {
            Destroy(gameObject);
            GSM.SetCurrentState(GameStateManager.GameState.Lose);
        }

        return _currentHp;
    } 
}
