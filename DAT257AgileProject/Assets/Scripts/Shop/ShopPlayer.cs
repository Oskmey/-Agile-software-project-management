using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayer : MonoBehaviour
{
    private PlayerStatsManager playerStatsManager;

    private void Start()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    public void TryToBuy(AccessorySO type)
    {
        if (playerStatsManager.Money >= type.cost)
        {
            playerStatsManager.Money -= type.cost;
            AddItemToInventory(type);
        }
    }

    private void AddItemToInventory(AccessorySO type)
    {
        // TODO Add to inventory
        Debug.Log(type.accessoryName + " is going to be added to your inventory");
    }
}
