using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public float speed;
   
    public Rigidbody2D rb;

    void Start()
    { 
        rb.velocity = transform.up * speed;
    }

    public void Update()
    {
        CheckIfOutOSight();
    }

    public void CheckIfOutOSight()
    {
        if (transform.position.y > 2.0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy)
        {
            FindObjectOfType<GameController>().EnemyIsHit();
            Destroy(gameObject);
        }
    }
}
