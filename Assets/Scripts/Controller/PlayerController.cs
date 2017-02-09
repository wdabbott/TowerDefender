using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Turret;

    private PlayerState state;
    private GameObject newTower;
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
    }

    private void PlaceTurret()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newTower.GetComponent<Rigidbody2D>().position = mousePosition;
        if (typePlacing == 1)
        {
            newTower.GetComponent<TowerController>().IsPlaced = true;
        }
        typePlacing = 0;
        IsPlacing = false;
    }

    private void ReturnToInventory()
    {
        if (newTower != null)
        {
            Destroy(newTower.gameObject);
            state.TowerInventory.Add(new GameObject("Tower"));
            typePlacing = 0;
            IsPlacing = false;
        }
    }

    public void CreateTower(int turretType)
    {
        if (!IsPlacing)
        {
            newTower = Instantiate(Turret, new Vector3(0, 0, 0), Quaternion.identity);
            newTower.GetComponent<TowerController>().IsPlaced = false;
            IsPlacing = true;
            typePlacing = turretType;
        }
    }
}
