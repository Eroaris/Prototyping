using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 moveInput;
    public float speed = 5f;
    public int health = 3;
    private int _currentHp;
    private float _lastHitTime;
    private const float IFrameDuration = 2;

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
            print("Damage taken!");
        }
        
        if (_currentHp <= 0)
        {
            Destroy(gameObject);
        }

        return _currentHp;
    } 
}
