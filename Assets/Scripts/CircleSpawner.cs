using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleSpawner : MonoBehaviour
{
    private Vector3 _randomSpawnPosition;
    public SlimeEnemy slimePrefab;
    public ExplodyEnemy explodyPrefab;
    public Camera camera;

    private void Start()
    {
        SpawnSlime(1);
    }

    Vector3 RandomPointInBounds()
    {
        
        Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 1, 10));
        
        
        return _randomSpawnPosition;
    }
    
    public void SpawnSlime(int count)
    {
        while (count > 0)
        {
            _randomSpawnPosition = RandomPointInBounds(); 
            Instantiate(explodyPrefab, _randomSpawnPosition, Quaternion.identity);
            count--;
        }
    }
    public void SpawnExplody()
    {
        _randomSpawnPosition = RandomPointInBounds();
        Instantiate(explodyPrefab, _randomSpawnPosition, Quaternion.identity);
    }
}
