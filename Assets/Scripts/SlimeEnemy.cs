using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public int damage = 2;
    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player;
        player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ApplyDamage(damage);
        }
    }
}
