using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopPlayer : MonoBehaviour
{
    private int money;
    [SerializeField]
    private InventorySO inventoryData;
    private PlayerStatsManager playerStatsManager;

    void Start()
    {
        playerStatsManager = FindAnyObjectByType<PlayerStatsManager>();
    }

    public void TryToBuy(AccessorySO type)
    {
        money = PlayerPrefs.GetInt("Money");
        Debug.Log("You have: " + money + " money");

        if (inventoryData.IsInventoryFull())
        {
            Debug.Log("Inventory is full, can not buy item");
        }
        else if (money >= type.cost)
        {
            Debug.Log("You had enough money!");
            AddItemToInventory(type);
            PlayerPrefs.SetInt("Money", money - type.cost);
            playerStatsManager.Money -= type.cost;
            Debug.Log("You know have: " + PlayerPrefs.GetInt("Money") + " money left");
        }
        else
        {
            Debug.Log("You are poor!");
        }
    }

    private void AddItemToInventory(AccessorySO type)
    {
        EquippableItemSO[] equippableItems = Resources.LoadAll<EquippableItemSO>("");
        List<EquippableItemSO> equippableItemsList = equippableItems.ToList();

        EquippableItemSO matchingItem = equippableItemsList.Find(item => item.Accessory == type);

        if (matchingItem != null)
        {
            int remainder = inventoryData.AddItem(matchingItem, 1);
        }
        else
        {
            Debug.LogError("No matching item found.");
        }
    }
}
