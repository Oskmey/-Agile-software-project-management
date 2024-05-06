using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [Serializable]
    public struct InventoryItem
    {
        [SerializeField]
        private int quantity;
        [SerializeField]
        private ItemSO item;
        [SerializeField]
        private List<ItemParameter> itemState;

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

        public List<ItemParameter> ItemState
        {
            get { return itemState; }
            private set { itemState = value; }
        }


        public InventoryItem(ItemSO item, int quantity, List<ItemParameter> itemState)
        {
            this.item = item;
            this.quantity = quantity;
            this.itemState = itemState;
        }

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem(Item, newQuantity, new(itemState));
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem(null, 0, new());
    }
}