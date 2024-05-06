using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem inventoryItemPrefab;

        [SerializeField]
        private UIAccessoryItem accessoryPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private RectTransform accessoryPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        private List<UIItem> listOfInventoryUIItems;
        private List<UIItem> listOfAccessoryUIItems;

        [SerializeField]
        private MouseFollower mouseFollower;

        public event Action<int, UIItem> OnDescriptionRequested,
            OnItemActionRequested, OnStartDragging;

        public event Action<int, int, UIItem, UIItem> OnSwapItems;

        private int currentlyDraggedItemIndex = -1;
        private UIItem currentlyDraggedItemUI;

        [SerializeField]
        private ItemActionPanel actionPanel;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            listOfInventoryUIItems = new();
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                listOfInventoryUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                // Note: Leftover code from tutorial for action buttons
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        public void InitializeAccessoryUI(int accessorySize)
        {
            listOfAccessoryUIItems = new();
            for (int i = 0; i < accessorySize; i++)
            {
                UIAccessoryItem uiItem = Instantiate(accessoryPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(accessoryPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                listOfAccessoryUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity, bool isAccessory)
        {
            if (listOfInventoryUIItems.Count > itemIndex && !isAccessory)
            {
                listOfInventoryUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
            else if(listOfAccessoryUIItems.Count > itemIndex && isAccessory)
            {
                listOfAccessoryUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIItem itemUI)
        {
            int index = -1;
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                index = listOfInventoryUIItems.IndexOf(uIInventoryItem);
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                index = listOfAccessoryUIItems.IndexOf(uIAccessoryItem);
            }

            if (index == -1)
            {
                return;
            }

            //
            OnItemActionRequested?.Invoke(index, itemUI);
        }

        private void HandleEndDrag(UIItem itemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIItem itemUI)
        {
            int index = -1;
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                index = listOfInventoryUIItems.IndexOf(uIInventoryItem);
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                index = listOfAccessoryUIItems.IndexOf(uIAccessoryItem);
            }

            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index, currentlyDraggedItemUI, itemUI);
            HandleItemSelection(itemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            currentlyDraggedItemUI = null;
        }

        private void HandleBeginDrag(UIItem itemUI)
        {
            int index = -1;
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                index = listOfInventoryUIItems.IndexOf(uIInventoryItem);
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                index = listOfAccessoryUIItems.IndexOf(uIAccessoryItem);
            }
            if (index == -1)
            {
                return;
            }
            //
            currentlyDraggedItemIndex = index;
            currentlyDraggedItemUI = itemUI;
            HandleItemSelection(itemUI);
            //
            OnStartDragging?.Invoke(index, itemUI);
        }

        public void CreateDraggeditem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }
        private void HandleItemSelection(UIItem itemUI)
        {
            int index = -1;
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                index = listOfInventoryUIItems.IndexOf(uIInventoryItem);
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                index = listOfAccessoryUIItems.IndexOf(uIAccessoryItem);
            }

            if (index == -1)
            {
                return;
            }
            //
            OnDescriptionRequested?.Invoke(index, itemUI);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButon(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex, UIItem itemUI)
        {
            actionPanel.Toggle(true);
            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                actionPanel.transform.position = listOfInventoryUIItems[itemIndex].transform.position;
            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                actionPanel.transform.position = listOfAccessoryUIItems[itemIndex].transform.position;
            }
        }

        private void DeselectAllItems()
        {
            foreach (UIItem item in listOfInventoryUIItems)
            {
                item.Deselect();
            }

            foreach (UIItem item in listOfAccessoryUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description, UIItem itemUI)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();

            if (itemUI is UIInventoryItem uIInventoryItem)
            {
                listOfInventoryUIItems[itemIndex].Select();

            }
            else if (itemUI is UIAccessoryItem uIAccessoryItem)
            {
                listOfAccessoryUIItems[itemIndex].Select();

            }
        }

        private void OnDestroy()
        {
            listOfAccessoryUIItems.Clear();
            listOfInventoryUIItems.Clear();
        }

        internal void ResetAccessoryItems()
        {
            foreach (UIItem item in listOfAccessoryUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void ResetInventoryItems()
        {
            foreach (UIItem item in listOfInventoryUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}