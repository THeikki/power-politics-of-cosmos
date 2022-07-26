using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    
    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
