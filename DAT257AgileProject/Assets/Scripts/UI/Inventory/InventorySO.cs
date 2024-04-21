using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : MonoBehaviour
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    InventorySO val = null;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void Additem(ItemSO item, int quantity)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = new InventoryItem(item, quantity);
                break;
            }
        }
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

        for(int i = 0;i < inventoryItems.Count;i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                continue;
            }
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }
}

[Serializable]
public struct InventoryItem
{
    public int Quantity { get; private set; }
    public ItemSO Item { get; private set; }
    public bool IsEmpty => Item == null;

    public InventoryItem(ItemSO item, int quantity)
    {
        Item = item;
        Quantity = Mathf.Max(0, quantity);
    }

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem(Item, newQuantity);
    }

    public static InventoryItem GetEmptyItem()
    => new InventoryItem
    {
        Item = null,
        Quantity = 0,
    };
}
