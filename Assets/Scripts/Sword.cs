using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private CapsuleCollider2D _myCollider2D;
    private SpriteRenderer _spriteRenderer;
    private Animator anim;
    public GameStateManager GSM;
    
    private Vector3 _currentDirection;
    public bool canUpgrade;
    public Player player;
    public Vector2 difference;
    private float swordLength;
    
    
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
            case GameStateManager.GameState.LevelUP:
                canUpgrade = true;
                break;
            
        }
    }
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myCollider2D = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        swordLength = transform.localScale.x;
        canUpgrade = false;
    }

    void Update()
    {
        if (player.moveInput != Vector3.zero)
        {
            _currentDirection = player.moveInput;
        }
        
        _myCollider2D.enabled = !_myCollider2D.enabled;
        Animate();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                enemy.ApplyDamage(player.damage);
                difference = enemy.transform.position - player.transform.position;
                difference = difference.normalized * player.knockBackPower;
                enemy.ReceiveKnockback(difference, player.knockBackTime, col);
            }
    } 
    
    public void UpgradeRange()
    {
        if (canUpgrade)
        {
            swordLength *= 1.5f;
            print(swordLength);
            canUpgrade = false;
            GSM.SetCurrentState(GameStateManager.GameState.Playing);
        }
    }
    
    public void resetAttack()
    {
        player._cooldownTimer =  player.attackCooldown;
    }  
    void Animate()
    {
        anim.SetFloat("AnimMoveX",_currentDirection.x);
        anim.SetFloat("AnimMoveY",_currentDirection.y);
    }
}