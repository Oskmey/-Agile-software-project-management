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

    private  List<UIInventoryItem> listOfUIitems = new();
    // Test mock variables for items
    public Sprite image;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        //Hide();
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIitems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseButtonClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {

    }

    private void HandleEndDrag(UIInventoryItem item)
    {
    }

    private void HandleSwap(UIInventoryItem item)
    {
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        itemDescription.SetDescription(image, title, description);
        listOfUIitems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        listOfUIitems[0].SetData(image, quantity);
        //DeselectAllItems();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
