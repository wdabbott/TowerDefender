using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAi : MonoBehaviour {

    public GameObject target;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int fireTimer;
    public bool IsPlaced;
    public AudioSource ShootSound;

    private string enemy;
    private int timeSinceFire = 0;

    // Use this for initialization
    void Start()
    {
        ShootSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsPlaced)
        {
            target = findNearestEnemy();

            if (target != null)
            {
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg) + 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
            }
            
            timeSinceFire++;

            if (timeSinceFire >= fireTimer && target != null)
            {
                Fire();
                timeSinceFire = 0;
            }
        }
        else
        {
            //Debug.Log(Input.mousePosition.x);
            this.GetComponent<Rigidbody2D>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    GameObject findNearestEnemy()
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestObject = null;
        foreach (var enemy in objectsWithTag)
        {
            if (closestObject == null)
            {
                closestObject = enemy;
            }
            //compares distances
            if (closestObject != null && Vector3.Distance(transform.position, enemy.transform.position) 
                <= Vector3.Distance(transform.position, closestObject.transform.position))
            {
                closestObject = enemy;
            }
        }
        
        return closestObject;
    }

    void Fire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        
        bullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position) * 10;

        ShootSound.Play();

        Destroy(bullet, 2.0f);
    }
}
