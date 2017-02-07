using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandleGui : MonoBehaviour
{
    public int TurretCount;
    public int EnchancersCount;
    public int ConsumablesCount;

    private PlayerState state;
    private HandleMainPlayer playerController;
    private int inventoryItemWidth = 50;

    void Start()
    {
        var playerObject = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();

        state = playerObject.GetComponent<PlayerState>();
        playerController = playerObject.GetComponent<HandleMainPlayer>();
    }
    void OnGUI()
    {
        DrawTowers();
        //DrawConsumables();
        //DrawEnhancers();
    }

    private void DrawTowers()
    {
        var leftSide = (Screen.width - (TurretCount*inventoryItemWidth))/2;

        for (int i = 0; i < TurretCount; i++)
        {
            if (i < state.TowerInventory.Count)
            {
                if (
                    GUI.Button(
                        new Rect(leftSide + inventoryItemWidth*i, Screen.height - inventoryItemWidth, inventoryItemWidth,
                            inventoryItemWidth), state.TowerInventory[i].name))
                {
                    state.TowerInventory.RemoveAt(i);
                    playerController.CreateTower(1);
                }
            }
        }
    }

    private void DrawConsumables()
    {
        for (int i = 0; i < ConsumablesCount; i++)
        {
            if (GUI.Button(new Rect(inventoryItemWidth*i, inventoryItemWidth*2, inventoryItemWidth, inventoryItemWidth),
                "Bomb"))
            {
                GUI.color = new Color(0, 0, 0, 0f);
            }
        }
    }

    private void DrawEnhancers()
    {
        for (int i = 0; i < EnchancersCount; i++)
        {
            if (GUI.Button(new Rect(inventoryItemWidth*i, inventoryItemWidth, inventoryItemWidth, inventoryItemWidth),
                "Speed"))
            {
            }
        }
    }
}
