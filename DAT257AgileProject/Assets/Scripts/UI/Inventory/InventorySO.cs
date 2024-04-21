using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

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

    public InventoryItem GetItemAt(int itemIndex)
    {
       return inventoryItems[itemIndex];
    }
}

[Serializable]
public struct InventoryItem
{
    [SerializeField]
    private int quantity;
    [SerializeField]
    private ItemSO item;


    public int Quantity
    {
        get { return quantity; }
        private set { quantity = Mathf.Max(0, value); } 
    }

    public ItemSO Item
    {
        get { return item; }
        private set { item = value; }
    }
    public bool IsEmpty => Item == null;

    public InventoryItem(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = Mathf.Max(0, quantity);
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