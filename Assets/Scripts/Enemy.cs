using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSound;
    public AudioClip laserSound;
    public AudioClip explotionSound;
    public Transform firePoint;
    public GameObject enemy_laser;
    private Animator anim;

    public float laserSpeed = 2f;

    //public bool enemyIsAlive;
    
    float firerate = 1f;
    float nextfire;

    void Start()
    {
        audioSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nextfire = Time.time;
        FirstFire();
        //enemyIsAlive = true;
    }

    void Update()
    {
        CheckIfTimeToFire();      
    }

    public void SetLaserSpeed(float speed)
    {
        laserSpeed = speed;
    }

    public void SetFireRate(float rate)
    {
        firerate = rate;
    }
   
    public void FirstFire()
    {
        nextfire = 1f + Time.time;
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextfire)
        {
            Instantiate(enemy_laser, firePoint.position, firePoint.rotation);
            PlayShootingSound();
            nextfire = Time.time + firerate;
        }
    }

    void PlayShootingSound()
    {
        audioSound.clip = laserSound;
        audioSound.PlayOneShot(audioSound.clip);
    }

    void PlayExplodingSound()
    {
        audioSound.clip = explotionSound;
        audioSound.PlayOneShot(audioSound.clip);
    }

    public void Explode()
    {
        FindObjectOfType<Points>().AddPoint(10); 
        anim.Play("Enemy_explotion");
        PlayExplodingSound();
        Destroy(gameObject, 0.5f);
    }
}
