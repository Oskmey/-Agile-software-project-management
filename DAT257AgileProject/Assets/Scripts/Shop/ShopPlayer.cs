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
            AudioManager.Instance.PlaySound(SoundName.ItemBought);
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

        if (playerStatsManager.PurchasedAccessories.ContainsKey(matchingItem.Accessory))
        {
            playerStatsManager.PurchasedAccessories[matchingItem.Accessory]++;
        }
        else
        {
            playerStatsManager.PurchasedAccessories.Add(matchingItem.Accessory, 1);
        }
    }
}
