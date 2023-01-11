using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider2D myCollider2D;
    public Vector3 moveInput;
    public float speed = 5f;
    public int health = 3;
    private bool canTakeDamage = true;
    private int _currentHp;
    private float lastHitTime = 0;
    private float iFrameDuration = 2;
    
    void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
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
        float check = lastHitTime+iFrameDuration;
        if (check < Time.realtimeSinceStartup)
        {
            _currentHp -= damageAmount;
            lastHitTime = Time.realtimeSinceStartup;
            print("Damage taken!");
        }
        
        if (_currentHp <= 0)
        {
            Destroy(gameObject);
        }

        return _currentHp;
    } 
}
