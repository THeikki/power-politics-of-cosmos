using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoint;
    
    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int randomPoint = Mathf.RoundToInt(Random.Range(0f, spawnPoint.Length - 1));
        Instantiate(enemyPrefab, spawnPoint[randomPoint].transform.position, Quaternion.identity);
    }
}
