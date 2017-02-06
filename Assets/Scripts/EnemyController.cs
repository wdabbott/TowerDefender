using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public AudioClip splat;
    public GameObject Loot;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Bullet")
        {
            health -= 1;
            Destroy(coll.gameObject);
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            GetComponent<AudioSource>().clip = splat;
            GetComponent<AudioSource>().Play();

            Instantiate(Loot, transform.position, Quaternion.identity);
            
            Destroy(this.gameObject);
        }
    }
}
