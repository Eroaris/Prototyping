using System;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private Enemy _enemy;
    public int damage = 2;
    public static int slimeCount;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        slimeCount++;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ApplyDamage(damage);
        }
    }
}
