using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    void awake()
    {
        transform.position = new Vector2(0, 0);
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (transform.position.x >= 1.5)
        {
            transform.position = new Vector2(1.5f, transform.position.y);
        }

        else if (transform.position.x <= -1.5)
        {
            transform.position = new Vector2(-1.5f, transform.position.y);
        }

        if (transform.position.y >= 2.18)
        {
            transform.position = new Vector2(transform.position.x, 2.18f);
        }

        else if (transform.position.y <= -1.7)
        {
            transform.position = new Vector2(transform.position.x, -1.7f);

        }
    }
}
