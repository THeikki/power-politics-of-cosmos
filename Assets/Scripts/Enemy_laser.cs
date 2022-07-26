using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_laser : MonoBehaviour
{
    static int missedShots = 0;

    public Rigidbody2D rb;
    Spacecraft target;
    Vector2 direction;

    void Start()
    {
        target = FindObjectOfType<Spacecraft>();
        TryToHit();
    }

    void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    void TryToHit()
    {
        try
        {
            rb.velocity = transform.up * FindObjectOfType<Enemy>().laserSpeed;
            direction = (target.transform.position - transform.position).normalized * FindObjectOfType<Enemy>().laserSpeed;
            rb.velocity = new Vector2(direction.x, direction.y);
            Destroy(gameObject, 3f);
        }
        catch
        {
            if (missedShots >= 1)
            {
                GameOver();
                missedShots = 0;
            }
        }
        
        missedShots += 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Spacecraft player = collision.GetComponent<Spacecraft>();

        if (player)
        {
            FindObjectOfType<GameController>().PlayerIsHit();
            Destroy(gameObject);
        }
    }
}
