using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayer : MonoBehaviour
{
    private int money;
    private PlayerStatsManager playerStatsManager;

    void Start()
    {
        playerStatsManager = FindAnyObjectByType<PlayerStatsManager>();
    }

    public void TryToBuy(AccessorySO type)
    {
        money = PlayerPrefs.GetInt("Money");
        Debug.Log("You have: " + money + " money");

        if (money >= type.cost)
        {
            Debug.Log("You had enough money!");
            addItemToInventory(type);
            PlayerPrefs.SetInt("Money", money - type.cost);
            playerStatsManager.Money -= type.cost;
            Debug.Log("You know have: " + PlayerPrefs.GetInt("Money") + " money left");
        }
        else
        {
            Debug.Log("You are poor!");
        }
    }

    private void addItemToInventory(AccessorySO type)
    {
        // TODO Add to inventory
        Debug.Log(type.accessoryName + " is going to be added to your inventory");
    }
}
