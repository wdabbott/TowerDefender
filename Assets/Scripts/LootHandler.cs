using System.Collections;
using System.Collections.Generic;
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

            if (hit)
            {
                PlayerState.inventory.Add(new GameObject("Loot"));
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
