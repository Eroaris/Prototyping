using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider2D myCollider2D;
    public Vector3 moveInput;
    public float speed = 5f;
    public int health = 3;
    private int _currentHp;
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
        _currentHp -= damageAmount;
       
        if (_currentHp <= 0)
        {
            Destroy(gameObject);
        }

        return _currentHp;
    } 
}
