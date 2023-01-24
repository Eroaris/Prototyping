using UnityEngine;
public class SlimeEnemy : MonoBehaviour
{
    private Enemy _enemy;
    public Animator anim;
    public int damage = 2;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Animate();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ApplyDamage(damage);
        }
    }
    void Animate()
    {
        if (anim.enabled)
        {
            anim.SetFloat("SlimeAnimX",_enemy.myRigidbody.velocity.x);
            anim.SetFloat("SlimeAnimY",_enemy.myRigidbody.velocity.y);
        }
    }
}
