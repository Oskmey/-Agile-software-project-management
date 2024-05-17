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
        }
        else if (money < type.cost)
        {
            OnBuyNotEnoughMoney?.Invoke();
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
        if (matchingItem == null)
        {
            Debug.LogError("UpdatePlayerStatsMaps failed: matchingItem is null.");
            return;
        }

        if (playerStatsManager.PurchasedMaps.Contains(matchingItem))
        {
            GameObject.Find("GameplayHUD").transform.Find("WarningPopUp").GetComponent<WarningPopup>().DisplayWarning("You already have this item!");
        }
        else
        {
            playerStatsManager.PurchasedMaps.Add(matchingItem);
            ProcessPayment(matchingItem.Accessory);
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
            ProcessPayment(matchingItem.Accessory);
        }
        else
        {
            Debug.LogError("No matching item found.");
        }
    }

    private void ProcessPayment(AccessorySO type)
    {
        playerStatsManager.CurrentMoney -= type.cost;
        playerStatsManager.TotalMoneySpent += type.cost;
    }

}
