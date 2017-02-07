using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMainPlayer : MonoBehaviour
{
    public GameObject Turret;

    private GameObject newTurret;
    private bool IsPlacing = false;
    private int typePlacing = 0;

	void Update ()
	{
        if (Input.GetButtonDown("1") && !IsPlacing)
	    {
            CreateTower(1);
	    }

        if (Input.GetMouseButtonDown(0) && IsPlacing)
	    {
            PlaceTurret();
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
