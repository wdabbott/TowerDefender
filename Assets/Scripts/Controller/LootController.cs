using System;
using System.Linq;
using UnityEngine;

public class LootController : MonoBehaviour
{

    public PlayerState PlayerState;

	void Start () {
        PlayerState = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault().GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit && hit.transform.gameObject.tag == "Loot")
            {
                var number = UnityEngine.Random.Range(0, 3);

                switch (number)
                {
                    case 0:
                        AddTower();
                        break;
                    case 1:
                        AddEnhancer();
                        break;
                    case 2:
                        AddConsumable();
                        break;
                }

                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void AddConsumable()
    {
        PlayerState.ConsumableInventory.Add(new GameObject("Consumable"));
        
    }

    private void AddEnhancer()
    {
        PlayerState.EnhancerInventory.Add(new GameObject("Enchancer"));
    }

    private void AddTower()
    {
        PlayerState.TowerInventory.Add(new GameObject("Tower"));
    }
}
