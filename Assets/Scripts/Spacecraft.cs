using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spacecraft : MonoBehaviour
{
    public AudioSource audioSound;
    public AudioClip laserSound;
    public AudioClip explotionSound;
    private Animator anim;
    public Transform firePoint;
    public Enemy cpu;
    public GameObject laser;
    
    public bool playerIsAlive;

    public void Awake()
    {
        playerIsAlive = true;
    }

    private void Start()
    {
        audioSound = GetComponent<AudioSource>();
        cpu = FindObjectOfType<Enemy>();
        anim = GetComponent<Animator>();      
    }

    void Update()
    {
        CheckIfCanFire();
    }

    public void CheckIfCanFire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsAlive == true)
        {
            Shoot();
        }
    }

    void PlayShootingSound()
    {
        audioSound.clip = laserSound;
        audioSound.PlayOneShot (audioSound.clip);
    }

    void PlayExplodingSound()
    {
        audioSound.clip = explotionSound;
        audioSound.PlayOneShot(audioSound.clip);
    }

    void Shoot()
    {
        Instantiate(laser, firePoint.position, firePoint.rotation);
        PlayShootingSound();
    }
    
    public void Explode()
    {
        anim.Play("Hero_explotion");
        PlayExplodingSound();
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            playerIsAlive = false;
            Explode();
        }
    }
}
