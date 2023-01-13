using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    private Collider2D myCollider2D;
    private Vector3 _randomSpawnPosition;
    public SlimeEnemy slimePrefab;
    public ExplodyEnemy explodyPrefab;
    

    public void SpawnSlime(int count)
    {
        while (count > 0)
        {
            _randomSpawnPosition = RandomPointInBounds(myCollider2D.bounds);
            Instantiate(slimePrefab, _randomSpawnPosition, Quaternion.identity);
            --count;
        }
    }
    public void SpawnExplody(int count)
    {
        while (count > 0)
        {
            _randomSpawnPosition = RandomPointInBounds(myCollider2D.bounds);
            Instantiate(explodyPrefab, _randomSpawnPosition, Quaternion.identity);
            --count;
        }
    }
    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return  new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }
}
