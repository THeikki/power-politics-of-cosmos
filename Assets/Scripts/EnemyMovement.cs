using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Enemy cpu;
    public bool isDestroyable;

    void Start()
    {
        cpu = FindObjectOfType<Enemy>();
        isDestroyable = false;
        speed = .5f;   
    }

    void Update()
    {
        Fly();
        CheckIfDestoyable();
    }

    public void CheckIfDestoyable()
    {
        if(transform.position.y < 2.2)
        {
            isDestroyable = true;
        }
    }

    void Fly()
    {
        
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y <= -3)
        {
            Destroy(gameObject);
            FindObjectOfType<GameController>().EnemyIsOutOfBoundary();

        }
    }
}
