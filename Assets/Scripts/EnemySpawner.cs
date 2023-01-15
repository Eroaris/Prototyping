using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    private Collider2D myCollider2D;
    private Vector3 _randomSpawnPosition;
    public GameObject slimePrefab;
    public GameObject explodyPrefab;
    private float slimeInterval = 2f;
    private float explodyInterval = 10f;
    
    private int maxEnemies;
    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy(slimeInterval,slimePrefab));
        StartCoroutine(SpawnEnemy(explodyInterval, explodyPrefab ));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        
        _randomSpawnPosition = RandomPointInBounds(myCollider2D.bounds);
       Instantiate(enemy, _randomSpawnPosition, Quaternion.identity);
       StartCoroutine(SpawnEnemy(interval, enemy));
    }
    
    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return  new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));
    }
}

