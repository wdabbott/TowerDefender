using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public AudioClip splat;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Bullet")
        {
            health -= 1;
            Destroy(coll.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update()
    {
        if (health <= 0)
        {
            GetComponent<AudioSource>().clip = splat;
            GetComponent<AudioSource>().Play();
            
            Destroy(this.gameObject);
        }
    }
}
