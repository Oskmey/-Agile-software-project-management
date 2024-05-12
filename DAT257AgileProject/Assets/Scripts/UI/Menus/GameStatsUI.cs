using Inventory;
using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class GameStatsUI : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private InventorySO accessoryData;

    private PlayerStatsManager playerStatsManager;

    [Header ("Money stats")]
    [SerializeField]
    private TextMeshProUGUI currentMoneyText;
    [SerializeField]
    private TextMeshProUGUI totalMoneyGainedText;
    [SerializeField]
    private TextMeshProUGUI totalMoneySpentText;
    [SerializeField]
    private TextMeshProUGUI accessoriesPurchasedText;

    [Header("Inventory stats")]
    [SerializeField]
    private TextMeshProUGUI trashCaughtText;
    [SerializeField]
    private TextMeshProUGUI currentInventoryItemsText;
    [SerializeField]
    private TextMeshProUGUI currentlyEquippedAccessoriesText;
    [SerializeField]
    private TextMeshProUGUI recycledTrashText;
    private void Awake()
    {
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        UpdateStats();
    }
    private void OnEnable()
    {
        UpdateStats();
    }
    private void UpdateStats()
    {
        UpdateInventoryInfo();
        UpdateAccessoryInfo();
        UpdateMoneyInfo();
        UpdateRecycledTrashInfo();
        UpdateTrashCaughtInfo();
        UpdateAccessoriesPurchasedInfo();
    }
    
    private void UpdateTrashCaughtInfo()
    {
        Dictionary<TrashType, int> recycledTrash = playerStatsManager.TrashCaughtDictionary;
        StringBuilder sb = new();

        sb.Append("Trash Caught:");
        sb.AppendLine();
        if (recycledTrash.Count > 0 )
        {
            foreach (var trash in recycledTrash)
            {
                sb.Append($"• {trash.Value}x {trash.Key.ToReadableString()}");
                sb.AppendLine();
            }
        }
        else
        {
            sb.Append("None");
            sb.AppendLine();
        }

        trashCaughtText.text = sb.ToString();
    }

    private void UpdateAccessoriesPurchasedInfo()
    {
        Dictionary<AccessorySO, int> purchasedAccessories = playerStatsManager.PurchasedAccessories;
        StringBuilder sb = new();

        sb.Append("Purchased accessories:");
        sb.AppendLine();

        if (purchasedAccessories.Count > 0)
        {
            foreach (var accessory in purchasedAccessories)
            {
                sb.Append($"• {accessory.Value}x {accessory.Key.AccessoryName}");
                sb.AppendLine();
            }
        }
        else
        {
            sb.Append("None");
            sb.AppendLine();
        }

        accessoriesPurchasedText.text = sb.ToString();
    }
    private void UpdateMoneyInfo()
    {
        currentMoneyText.text = $"Current Money: {playerStatsManager.CurrentMoney}";
        totalMoneyGainedText.text = $"Total Money Gained: {playerStatsManager.TotalMoneyEarned}";
        totalMoneySpentText.text = $"Total Money Spent: {playerStatsManager.TotalMoneySpent}";
    }

    private void UpdateRecycledTrashInfo()
    {
        Dictionary<TrashType, int> recycledTrash =  playerStatsManager.RecycledTrashDictionary;
        StringBuilder sb = new();

        sb.Append("Recycled from inventory:");
        sb.AppendLine();

        if (recycledTrash.Count > 0)
        {
            foreach (var trash in recycledTrash)
            {
                sb.Append($"• {trash.Value}x {trash.Key.ToReadableString()}");
                sb.AppendLine();
            }
        }
        else
        {
            sb.Append("None");
            sb.AppendLine();
        }

        recycledTrashText.text = sb.ToString();
    }

    private void UpdateInventoryInfo()
    {
        Dictionary<int, InventoryItem> inventoryDetails = inventoryData.GetCurrentInventoryState();
        StringBuilder sb = new();

        sb.Append("Current items in inventory:");
        sb.AppendLine();
        Dictionary<ItemSO, int> inventoryQuantity = new();

        if (inventoryDetails.Count > 0)
        {
            foreach (var inventoryItem in inventoryDetails)
            {
                ItemSO item = inventoryItem.Value.Item;
                int quantity = inventoryItem.Value.Quantity;

                if (inventoryQuantity.ContainsKey(item))
                {
                    inventoryQuantity[item] += quantity;
                }
                else
                {
                    inventoryQuantity.Add(item, quantity);
                }
            }

            foreach (var item in inventoryQuantity)
            {
                sb.Append($"• {item.Value}x {item.Key.Name}");
                sb.AppendLine();
            }
        }
        else
        {
            sb.Append("None");
            sb.AppendLine();
        }

        currentInventoryItemsText.text = sb.ToString();
    }

    private void UpdateAccessoryInfo()
    {
        Dictionary<int, InventoryItem> inventoryDetails = accessoryData.GetCurrentInventoryState();
        StringBuilder sb = new();

        sb.Append("Currently equipped accessories:");
        sb.AppendLine();
        Dictionary<ItemSO, int> inventoryQuantity = new();

        if (inventoryDetails.Count > 0)
        {
            foreach (var inventoryItem in inventoryDetails)
            {
                ItemSO item = inventoryItem.Value.Item;
                int quantity = inventoryItem.Value.Quantity;

                if (inventoryQuantity.ContainsKey(item))
                {
                    inventoryQuantity[item] += quantity;
                }
                else
                {
                    inventoryQuantity.Add(item, quantity);
                }
            }

            foreach (var item in inventoryQuantity)
            {
                sb.Append($"• {item.Value}x {item.Key.Name}");
                sb.AppendLine();
            }
        }
        else
        {
            sb.Append("None");
            sb.AppendLine();
        }

        currentlyEquippedAccessoriesText.text = sb.ToString();
    }
}