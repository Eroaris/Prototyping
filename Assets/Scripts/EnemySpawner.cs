using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    private Collider2D myCollider2D;
    private Vector3 _randomSpawnPosition;
    public GameObject slimePrefab;
    public GameObject explodyPrefab;
    private float slimeInterval = 2f;
    private float explodyInterval = 15f;
    public float minRadius;
    public float maxRadius;
    private int maxEnemies = 8;
    private int currentSlimes;
    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += WhenEnemyDestroyed;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= WhenEnemyDestroyed;
    }

    private void WhenEnemyDestroyed(Enemy enemy)
    {
        currentSlimes--;
        print(currentSlimes);
    }
    private void Start()
    {
        StartCoroutine(SpawnSlimes(slimeInterval,slimePrefab));
        StartCoroutine(SpawnExplody(explodyInterval, explodyPrefab ));
    }

    private IEnumerator SpawnExplody(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            
            _randomSpawnPosition = RandomPointonCircle(myCollider2D.bounds);
            Instantiate(enemy, _randomSpawnPosition + transform.position, Quaternion.identity);
        }
    }
    private IEnumerator SpawnSlimes(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            
                _randomSpawnPosition = RandomPointonCircle(myCollider2D.bounds);
                Instantiate(enemy, _randomSpawnPosition + transform.position, Quaternion.identity);
                currentSlimes++;
            
        }
    }
    private Vector3 RandomPointonCircle(Bounds bounds)
    {
        float randomAngle = Random.Range(0, 360);
        Vector2 randomPoint = new Vector2(
            Mathf.Cos(randomAngle),
            Mathf.Sin(randomAngle));

       float radius = Random.Range(minRadius, maxRadius);
        
       return randomPoint * radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,minRadius);
        Gizmos.DrawWireSphere(transform.position,maxRadius);
    }
}

