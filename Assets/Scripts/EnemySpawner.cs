using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private Collider2D myCollider2D;
    private Vector3 _randomSpawnPosition;
    public GameObject slimePrefab;
    public GameObject explodyPrefab;
    

    public int healthIncrease;
    private float slimeInterval = 1f;
    private float explodyInterval = 15f;
    public float minRadius;
    public float maxRadius;
    private float randomAngle;
    public int maxEnemies = 8;
    private int currentEnemies;
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
    public void WhenEnemyDestroyed(Enemy.EnemyState enemyState)
    {
        switch (enemyState)
        {
            case Enemy.EnemyState.Alive:
               
                break;
            
            case Enemy.EnemyState.Dead:
                currentEnemies--;
                break;
        }
    }
    private void Start()
    {
        StartCoroutine(SpawnSlimes(slimeInterval,slimePrefab));
        /*StartCoroutine(SpawnExplody(explodyInterval, explodyPrefab ));*/
    }

    private IEnumerator SpawnExplody(float interval, GameObject enemy)
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                yield return new WaitForSeconds(interval);
            
                _randomSpawnPosition = RandomPointOnCircle(myCollider2D.bounds);
                Instantiate(enemy, _randomSpawnPosition + transform.position, Quaternion.identity);
            }
        }
    }
    private IEnumerator SpawnSlimes(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            while (currentEnemies < maxEnemies)
            {
                _randomSpawnPosition = RandomPointOnCircle(myCollider2D.bounds);
                Instantiate(enemy, _randomSpawnPosition + transform.position, Quaternion.identity);
                currentEnemies++;
                interval = 10f;
            }
            maxEnemies++;
        }
    }
    private Vector3 RandomPointOnCircle(Bounds bounds)
    {
        
        /*if (_player.transform.position.x <= -29f)
        {
            randomAngle = Random.Range(0,180);
        }

        if (_player.transform.position.x >= 29f)
        {
            randomAngle = Random.Range(130,360);
        }*/
        
        randomAngle = Random.Range(0, 360);

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

