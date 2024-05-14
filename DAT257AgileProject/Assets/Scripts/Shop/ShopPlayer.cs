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
        money = playerStatsManager.Money;

        if (inventoryData.IsInventoryFull())
        {
            OnBuyNoFreeInventorySlot?.Invoke();
        }
        else if (money >= type.cost)
        {
            AddItemToInventory(type);
            playerStatsManager.Money -= type.cost;
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
        List<EquippableItemSO> equippableItemsList = equippableItems.ToList();
        Debug.Log(mapItems.Count());
        EquippableItemSO matchingItem = equippableItemsList.Find(item => item.Accessory == type);
        mapItemSO matchingMap = mapItems.ToList().Find(item => item.Accessory == type);

        if (matchingItem != null)
        {
            int remainder = inventoryData.AddItem(matchingItem, 1);
        }
        else if (matchingMap != null)
        {
            int remainder = inventoryData.AddItem(matchingMap, 1);
        }
        else
        {
            Debug.LogError("No matching item found.");
        }
    }
}
