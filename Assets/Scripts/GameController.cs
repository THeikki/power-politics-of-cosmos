using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static int enemyDeadCounter = 0;
    
    void Update()
    {
        CheckHowManyEnemiesHaveDied();
    }

    public void CheckHowManyEnemiesHaveDied()
    {
        if(enemyDeadCounter < 4)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(2f);
        }
        if(enemyDeadCounter >= 4)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(2.5f);           
        }
        if (enemyDeadCounter >= 8)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(2.8f);
            FindObjectOfType<Enemy>().SetFireRate(0.9f);
        }
        if (enemyDeadCounter >= 12)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(3.3f);
        }
        if (enemyDeadCounter >= 16)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(3.6f);
            FindObjectOfType<Enemy>().SetFireRate(0.7f);
        }
        if (enemyDeadCounter >= 20)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(4f);
        }
        if (enemyDeadCounter >= 30)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(4.6f);
            FindObjectOfType<Enemy>().SetFireRate(0.6f);
        }
        if (enemyDeadCounter >= 50)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(5.2f);
            FindObjectOfType<Enemy>().SetFireRate(0.5f);
        }
        if (enemyDeadCounter >= 60)
        {
            FindObjectOfType<Enemy>().SetLaserSpeed(5.6f);
            FindObjectOfType<Enemy>().SetFireRate(0.4f);
        }
    }

    public void PlayerIsHit()
    {
        enemyDeadCounter = 0;
        FindObjectOfType<Spacecraft>().playerIsAlive = false;             
        FindObjectOfType<Spacecraft>().Explode();
    }

    public void EnemyIsHit()
    {
        FindObjectOfType<EnemyMovement>().CheckIfDestoyable();
        

        if(FindObjectOfType<EnemyMovement>().isDestroyable == true)
        {
            FindObjectOfType<Enemy>().Explode();
            FindObjectOfType<EnemyController>().SpawnEnemy();
            enemyDeadCounter += 1;
        }
    }

    public void EnemyIsOutOfBoundary()
    {
        FindObjectOfType<EnemyController>().SpawnEnemy();
        enemyDeadCounter += 1;
    }
}
