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
        DrawConsumables();
        DrawEnhancers();
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
                        new Rect(leftSide + inventoryItemWidth*i, Screen.height - (inventoryItemWidth*2), inventoryItemWidth,
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
            if (i < state.ConsumableInventory.Count)
            {
                if (GUI.Button(new Rect(inventoryItemWidth * i, Screen.height - inventoryItemWidth, inventoryItemWidth, inventoryItemWidth),
                        "Bomb"))
                {
                    state.ConsumableInventory.RemoveAt(i);
                } 
            }
        }
    }

    private void DrawEnhancers()
    {
        var leftSide = Screen.width - (EnchancersCount * inventoryItemWidth);
        for (int i = 0; i < EnchancersCount; i++)
        {
            if (i < state.EnhancerInventory.Count)
            {
                if (GUI.Button(new Rect(leftSide + inventoryItemWidth * i, Screen.height - inventoryItemWidth, inventoryItemWidth, inventoryItemWidth),
                        "Speed"))
                {
                    state.EnhancerInventory.RemoveAt(i);
                } 
            }
        }
    }
}
