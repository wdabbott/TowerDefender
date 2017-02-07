using System;
using System.Linq;
using UnityEngine;

public class LootHandler : MonoBehaviour
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
                        PlayerState.TowerInventory.Add(new GameObject("Tower"));
                        Destroy(hit.transform.gameObject);
                        break;
                    case 1:
                        PlayerState.EnhancerInventory.Add(new GameObject("Enchancer"));
                        Destroy(hit.transform.gameObject);
                        break;
                    case 2:
                        PlayerState.ConsumableInventory.Add(new GameObject("Consumable"));
                        Destroy(hit.transform.gameObject);
                        break;
                }
            }
        }
    }
}
