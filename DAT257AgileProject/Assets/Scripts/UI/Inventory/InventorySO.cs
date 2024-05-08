using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory/InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 40;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public ReadOnlyCollection<InventoryItem> InventoryItems
        {
            get { return inventoryItems.AsReadOnly(); }
        }

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if(item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        if (inventoryItems[i].IsEmpty)
                        {
                            quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                        }
                    }
                    InformAboutChange();
                }
                return quantity;
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new(item, quantity, new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState));
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        public bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }

                if (inventoryItems[i].Item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].Item.MaxStackSize - inventoryItems[i].Quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].Item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].Quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while(quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.Quantity);
        }

        public void AddItemAt(InventoryItem item, int index)
        {
            if (inventoryItems[index].IsEmpty)
            {
                inventoryItems[index] = item;
            }
            InformAboutChange();
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new();

            for (int i = 0; i < inventoryItems.Count; i++)
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

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            //ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.

            (inventoryItems[itemIndex_2], inventoryItems[itemIndex_1]) = (inventoryItems[itemIndex_1], inventoryItems[itemIndex_2]);
            InformAboutChange();
        }

        public List<TrashItemSO> GetAndRemoveRecyclableTrashItems()
        {
            List<TrashItemSO> recyclableTrashItems = new();

            for (int i = inventoryItems.Count - 1; i >= 0; i--)
            {
                if (inventoryItems[i].Item is TrashItemSO trashItem)
                {
                    if (trashItem.TrashData.IsRecyclable)
                    {
                        int trashAmount = inventoryItems[i].Quantity;
                        for (int j = 0; j < trashAmount; j++)
                        {
                            recyclableTrashItems.Add(trashItem);
                        }
                        inventoryItems[i] = InventoryItem.GetEmptyItem();
                    }
                }
            }

            InformAboutChange();
            return recyclableTrashItems;
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)
                {
                    return;
                }

                int remainder = inventoryItems[itemIndex].Quantity - amount;
                if (remainder <= 0)
                {
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                }
                else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(remainder);
                }

                InformAboutChange();
            }
        }
    }
}