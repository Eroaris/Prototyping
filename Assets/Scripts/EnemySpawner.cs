using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    private Player _player;
    private Collider2D myCollider2D;
    private Vector3 _randomSpawnPosition;
    public GameObject slimePrefab;
    public GameObject explodyPrefab;
    private float slimeInterval = 10f;
    private float explodyInterval = 15f;
    public float minRadius;
    public float maxRadius;
    private float randomAngle;
    private int maxSlimes = 8;
    private int currentSlimes;
    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
        _player = GetComponentInParent<Player>();
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
            
            _randomSpawnPosition = RandomPointOnCircle(myCollider2D.bounds);
            Instantiate(enemy, _randomSpawnPosition + transform.position, Quaternion.identity);
            
        }
    }
    private IEnumerator SpawnSlimes(float interval, GameObject enemy)
    {
        while (true)
        {
            while (currentSlimes < maxSlimes)
            {
                _randomSpawnPosition = RandomPointOnCircle(myCollider2D.bounds);
                Instantiate(enemy, _randomSpawnPosition + transform.position, Quaternion.identity);
                currentSlimes++;
            }

            maxSlimes++;
            
            yield return new WaitForSeconds(interval);
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
        print("normal"); 
        
          
        
       
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

