using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandleMainPlayer : MonoBehaviour
{
    public GameObject Turret;

    private PlayerState state;
    private GameObject newTurret;
    private GameObject selectedEnemy;
    private bool IsPlacing = false;
    private int typePlacing = 0;

    void Start()
    {
        var playerObject = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();

        state = playerObject.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (Input.GetButtonDown("1") && !IsPlacing)
        {
            CreateTower(1);
        }

        if (Input.GetMouseButtonDown(0) && IsPlacing)
        {
            PlaceTurret();
        }

        if (Input.GetMouseButtonDown(1) && IsPlacing)
        {
            ReturnToInventory();
        }

        if (Input.GetMouseButtonDown(0))
        {
            SelectEnemy();
        }
    }

    private void SelectEnemy()
    {
        var rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

        if (hit && hit.transform.gameObject.tag == "Enemy")
        {
            if (selectedEnemy)
            {
                selectedEnemy.GetComponent<EnemyController>().SetSelected(false);
            }
            
            hit.transform.gameObject.GetComponent<EnemyController>().SetSelected(true);
            selectedEnemy = hit.transform.gameObject;
        }
    }

    private void PlaceTurret()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newTurret.GetComponent<Rigidbody2D>().position = mousePosition;
        if (typePlacing == 1)
        {
            newTurret.GetComponent<TurretAi>().IsPlaced = true;
        }
        typePlacing = 0;
        IsPlacing = false;
    }

    private void ReturnToInventory()
    {
        if (newTurret != null)
        {
            Destroy(newTurret.gameObject);
            state.TowerInventory.Add(new GameObject("Tower"));
            typePlacing = 0;
            IsPlacing = false;
        }
    }

    public void CreateTower(int turretType)
    {
        if (!IsPlacing)
        {
            newTurret = Instantiate(Turret, new Vector3(0, 0, 0), Quaternion.identity);
            newTurret.GetComponent<TurretAi>().IsPlaced = false;
            IsPlacing = true;
            typePlacing = turretType;
        }
    }
}
