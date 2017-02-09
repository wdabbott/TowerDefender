using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class TowerController : MonoBehaviour, ISelectable
{

    public GameObject target;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject TowerRangeGraphic;
    public int fireSpeed;
    public int range;
    public bool IsPlaced;
    public bool IsSelected;
    public AudioSource ShootSound;

    private string enemy;
    private int timeSinceFire = 0;
    private bool isSelected = false;

    // Use this for initialization
    void Start()
    {
        ShootSound = GetComponent<AudioSource>();
        InstantiateTowerRange();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsPlaced)
        {
            target = findNearestEnemy();

            TowerRangeGraphic.SetActive(false);

            if (target != null)
            {
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg) + 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
            }
            
            timeSinceFire++;

            if (timeSinceFire >= fireSpeed && target != null)
            {
                Fire();
                timeSinceFire = 0;
            }
        }
        else
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.GetComponent<Rigidbody2D>().position = mousePosition;
            TowerRangeGraphic.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    GameObject findNearestEnemy()
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestObject = null;
        foreach (var enemy in objectsWithTag)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= range)
            {
                if (closestObject == null)
                {
                    closestObject = enemy;
                }

                var distanceToClosestEnemy = Vector3.Distance(transform.position, closestObject.transform.position);
                
                if (distanceToEnemy <= distanceToClosestEnemy)
                {
                    closestObject = enemy;
                }
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

    private void InstantiateTowerRange()
    {
        TowerRangeGraphic = Instantiate(TowerRangeGraphic);
        var rangeSize = TowerRangeGraphic.transform.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        TowerRangeGraphic.transform.localScale = new Vector3(range * 2 / rangeSize.x, range * 2 / rangeSize.y);
    }

    public void SetSelected(bool select)
    {
        isSelected = select;
    }

    void OnGUI()
    {
        if (isSelected)
        {
            Vector2 targetPos;
            targetPos = Camera.main.WorldToScreenPoint(transform.position);

            GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 50, 20), fireSpeed.ToString());
        }
    }
}
