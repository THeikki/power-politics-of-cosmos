using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Spacecraft player;

    private void Start()
    {
        player = FindObjectOfType<Spacecraft>();
    }

    void Update()
    {
        CheckIfCanMove();
    }

    public void MoveControl()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed * Time.deltaTime;
        }

        transform.position = pos;

    }

    public void CheckIfCanMove()
    {
        if(player.playerIsAlive == true)
        {
            MoveControl();
        }
    }
}
