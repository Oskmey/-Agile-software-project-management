using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private InventorySO accessoryData;

        private PlayerInput playerInput;
        private InputAction showInventory;

        [SerializeField]
        private List<InventoryItem> initialInventoryItems = new();


        [SerializeField]
        private List<InventoryItem> initialAccessoryItems = new();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            showInventory = playerInput.actions["ShowInventory"];
            PrepareUI();
            PrepareInventoryData();
            PrepareAccessoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialInventoryItems)
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void PrepareAccessoryData()
        {
            accessoryData.Initialize();
            accessoryData.OnInventoryUpdated += UpdateAccessoriesUI;

            foreach (InventoryItem item in initialAccessoryItems)
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                accessoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            Debug.Log("inventory ui updated");

            inventoryUI.ResetInventoryItems();
            foreach (var item in inventoryState)    
            {
                inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity, false);
            }
        }

        private void UpdateAccessoriesUI(Dictionary<int, InventoryItem> inventoryState)
        {
            Debug.Log("accesories ui updated");
            inventoryUI.ResetAccessoryItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity, true);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.InitializeAccessoryUI(accessoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex, UIItem itemUI)
        {
            //
            UIItem itemSlot = null;
            InventorySO items = null;
            InventoryItem inventoryItem = default;
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                itemSlot = uIInventoryItem;
                items = inventoryData;
                inventoryItem = inventoryData.GetItemAt(itemIndex);
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                itemSlot = uIAccessoryItem;
                items = accessoryData;
                inventoryItem = accessoryData.GetItemAt(itemIndex);
            }

            if (inventoryItem.IsEmpty)
            {
                return;

            }

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if(itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex, itemSlot);
                if(itemAction.ActionName != null)
                {
                    inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex, items));
                }
            }

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Destroy all", () => DestroyItems(itemIndex, inventoryItem.Quantity, items));
            }
        }

        private void DestroyItems(int itemIndex, int quantity, InventorySO items)
        {
            items.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            //audioSource.PlayOneShot(dropClip);
        } 

        public void PerformAction(int itemIndex, InventorySO items)
        {
            InventoryItem inventoryItem = items.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }

            // equip här gör så att item tas bort
            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                items.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.ItemState);
                //audioSource.PlayOneShot(itemAction.actionSFX);
                if (items.GetItemAt(itemIndex).IsEmpty)
                {
                    inventoryUI.ResetSelection();
                }
            }
        }

        private void HandleDragging(int itemIndex, UIItem itemUI)
        {
            InventoryItem inventoryItem = default;

            if (itemUI is UIInventoryItem)
            {
                inventoryItem = inventoryData.GetItemAt(itemIndex);

            }
            else if (itemUI is UIAccessoryItem)
            {
                inventoryItem = accessoryData.GetItemAt(itemIndex);

            }

            if (inventoryItem.IsEmpty)
            {
                return;
            }
            inventoryUI.CreateDraggeditem(inventoryItem.Item.ItemImage, inventoryItem.Quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2, UIItem itemUI_1, UIItem itemUI_2)
        {
            // TODO
            // if both UIItem inventory
            if(itemUI_1 is UIInventoryItem && itemUI_2 is UIInventoryItem)
            {
                inventoryData.SwapItems(itemIndex_1, itemIndex_2);
            }
            else if (itemUI_1 is UIAccessoryItem && itemUI_2 is UIAccessoryItem)
            {
                accessoryData.SwapItems(itemIndex_1, itemIndex_2);
            }
            else if ((itemUI_1 is UIAccessoryItem && itemUI_2 is UIInventoryItem) || (itemUI_1 is UIInventoryItem && itemUI_2 is UIAccessoryItem))
            {

            }

            // if both UIItem accessory
            // if one accessory and one inventory
            // check which way, accessory to inventory or inventory to accessory
        }

        private void HandleDescriptionRequest(int itemIndex, UIItem itemUI)
        {
            UIItem itemSlot = null;
            InventoryItem inventoryItem = default;
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                itemSlot = uIInventoryItem;
                inventoryItem = inventoryData.GetItemAt(itemIndex);
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                itemSlot = uIAccessoryItem;
                inventoryItem = accessoryData.GetItemAt(itemIndex);
            }

            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.Item;
            string description = PrepareDescription(inventoryItem);

            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description, itemSlot);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new();
            sb.Append(inventoryItem.Item.Description);
            sb.AppendLine();
            if (inventoryItem.Item is TrashItemSO trashItem)
            {
                if (trashItem.TrashData.IsRecyclable)
                {
                    sb.Append("Is recyclable");
                }
                else
                {
                    sb.Append("Is not recyclable");
                }

                sb.AppendLine();
                sb.Append($"Money value: {trashItem.TrashData.MoneyValue}");
                sb.AppendLine();
                sb.Append($"Rarity: {TrashRarityExtensions.ToReadableString(trashItem.TrashRarity)}");
                sb.AppendLine();

                string categories = "";
                for (int i = 0; i < trashItem.TrashData.TrashCategories.Count; i++)
                {
                    TrashCategory trashCategory = trashItem.TrashData.TrashCategories[i];
                    if(i == trashItem.TrashData.TrashCategories.Count - 1)
                    {
                        categories += trashCategory;
                    }
                    else
                    {
                        categories += trashCategory + ", ";
                    }
                }
                sb.Append($"Trash Categories: {categories} ");
                sb.AppendLine();            
            }
            else if (inventoryItem.Item is EquippableItemSO equippableItem)
            {
                sb.Append($"Cost: {equippableItem.Accessory.cost} ");
                sb.AppendLine();

                sb.Append($"Rarity: {equippableItem.Accessory.rarity} ");
                sb.AppendLine();

                string effects = "";
                if (equippableItem.Accessory.accessoryEffects.Count == 0)
                {
                    effects = " none";
                }
                else
                {
                    for (int i = 0; i < equippableItem.Accessory.accessoryEffects.Count; i++)
                    {
                        AEffect accessoryEffect = equippableItem.Accessory.accessoryEffects[i];
                        if (i == equippableItem.Accessory.accessoryEffects.Count - 1)
                        {
                            effects += accessoryEffect;
                        }
                        else
                        {
                            effects += accessoryEffect + ", ";
                        }
                    }
                }

                sb.Append($"Accessory effects: {effects} ");
                sb.AppendLine();
            }
            else
            {
                for (int i = 0; i < inventoryItem.ItemState.Count; i++)
                {
                    sb.Append($"{inventoryItem.ItemState[i].GetItemParameter().ParameterName} " +
                        $": {inventoryItem.ItemState[i].Value} / " +
                        $"{inventoryItem.Item.DefaultParametersList[i].Value}");
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public void Update()
        {
            if(Time.timeScale > 0)
            {
                HandleInventoryShow();
            }
            else
            {
                inventoryUI.Hide();
            }
        }

        private void HandleInventoryShow()
        {
            if (showInventory.triggered)
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity, false);
                    }
                    //
                    foreach (var item in accessoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity, true);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}