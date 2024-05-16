using Inventory.Model;
using System;
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
    public delegate void ShopEvent();
    public event ShopEvent OnBuyNoFreeInventorySlot;
    public event ShopEvent OnBuyNotEnoughMoney;

    private void Start()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
        AudioManager.Instance.PlayMusic(MusicName.StoreTheme);
    }

    public void TryToBuy(AccessorySO type)
    {
        money = playerStatsManager.CurrentMoney;

        if (inventoryData.IsInventoryFull())
        {
            OnBuyNoFreeInventorySlot?.Invoke();
        }
        else if (money >= type.cost)
        {
            AddItemToInventory(type);
            playerStatsManager.CurrentMoney -= type.cost;
            playerStatsManager.TotalMoneySpent += type.cost;
        }
        else if (money < type.cost)
        {
            OnBuyNotEnoughMoney?.Invoke();
            //Debug.Log("You are poor!");
        }
    }

    private void AddItemToInventory(AccessorySO type)
    {
        EquippableItemSO[] equippableItems = Resources.LoadAll<EquippableItemSO>("");
        mapItemSO[] mapItems = Resources.LoadAll<mapItemSO>("");
        EquippableItemSO matchingItem = equippableItems.FirstOrDefault(item => item.Accessory == type);
        mapItemSO matchingMap = mapItems.FirstOrDefault(item => item.Accessory == type);
        if (matchingItem != null)
        {
            inventoryData.AddItem(matchingItem, 1);
            UpdatePlayerStats(matchingItem);
        }
        else if (matchingMap != null)
        {
            UpdatePlayerStatsMaps(matchingMap);
        }
        else
        {
            Debug.LogError("No matching item found.");
        }
    }

    private void UpdatePlayerStatsMaps(mapItemSO matchingItem)
    {
        Debug.Log(playerStatsManager.PurchasedMaps);
        if (matchingItem != null && !playerStatsManager.PurchasedMaps.Contains(matchingItem))
        {
            playerStatsManager.PurchasedMaps.Add(matchingItem);
        }
        else
        {
            Debug.LogError("No matching map found.");
        }
    }

    private void UpdatePlayerStats(EquippableItemSO matchingItem)
    {
        if (matchingItem != null)
        {
            if (playerStatsManager.PurchasedAccessories.ContainsKey(matchingItem.Accessory))
            {
                playerStatsManager.PurchasedAccessories[matchingItem.Accessory]++;
            }
            else
            {
                playerStatsManager.PurchasedAccessories.Add(matchingItem.Accessory, 1);
            }
        }
        else
        {
            Debug.LogError("No matching item found.");
        }
    }
}
