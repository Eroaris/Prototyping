using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    private Collider2D myCollider2D;
    private Vector3 _randomSpawnPosition;
    public SlimeEnemy slimePrefab;
    public ExplodyEnemy explodyPrefab;
    private int randomEnemy;
    private int maxEnemies = 1;
    private int allowedEnemies;
    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5f);
        
        allowedEnemies = maxEnemies;
        
        while (allowedEnemies > 0)
        {
            randomEnemy = Random.Range(0,99);

            if (randomEnemy > 15)
            {
                SpawnExplody();
            }
            else
            {
                SpawnSlime();
            }

            maxEnemies--;
        }

        maxEnemies++;
    }
    
    
    public void SpawnSlime()
    {
        _randomSpawnPosition = RandomPointInBounds(myCollider2D.bounds);
        Instantiate(slimePrefab, _randomSpawnPosition, Quaternion.identity);
            
    }
    public void SpawnExplody()
    {
        
        _randomSpawnPosition = RandomPointInBounds(myCollider2D.bounds);
        Instantiate(explodyPrefab, _randomSpawnPosition, Quaternion.identity);
            
        
    }
    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return  new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }
}

