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

        private PlayerInput playerInput;
        private InputAction showInventory;

        [SerializeField]
        private List<InventoryItem> initialItems = new();

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
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)    
            {
                inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;

            }

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if(itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.Quantity));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            //audioSource.PlayOneShot(dropClip);
        } 

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }

            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.Item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.ItemState);
                //audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    inventoryUI.ResetSelection();
                }
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }
            inventoryUI.CreateDraggeditem(inventoryItem.Item.ItemImage, inventoryItem.Quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.Item;
            string description = PrepareDescription(inventoryItem);

            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, description);
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
                        inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
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