using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    private  List<UIInventoryItem> listOfUIItems = new();

    [SerializeField]
    private MouseFollower mouseFollower;

    // Test mock variables for items
    public Sprite image, image2;
    public int quantity;
    public string title, description;

    public int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseButtonClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }

        currentlyDraggedItemIndex = index;
        mouseFollower.Toggle(true);
        mouseFollower.SetData(index == 0 ? image : image2, quantity);
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return;
        }
        listOfUIItems[currentlyDraggedItemIndex].SetData(index == 0 ? image : image2, quantity);
        listOfUIItems[currentlyDraggedItemIndex].SetData(currentlyDraggedItemIndex == 0 ? image : image2, quantity);
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        itemDescription.SetDescription(image, title, description);
        listOfUIItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        listOfUIItems[0].SetData(image, quantity);
        listOfUIItems[1].SetData(image2, quantity);
        //DeselectAllItems();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
