using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
    }

    private void Start()
    {
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg1), "Egg1", PurchasableItem.GetCost(PurchasableItem.Type.Egg1), 0);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 1);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg1), "Egg1", PurchasableItem.GetCost(PurchasableItem.Type.Egg1), 2);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 3);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg1), "Egg1", PurchasableItem.GetCost(PurchasableItem.Type.Egg1), 4);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 5);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 6);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 7);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 8);
        CreateShopButton(PurchasableItem.GetSprite(PurchasableItem.Type.Egg2), "Egg2", PurchasableItem.GetCost(PurchasableItem.Type.Egg2), 9);
    }

    private void CreateShopButton(Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopButton = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopButton.GetComponent<RectTransform>();

        // Positioning the button
        float shopItemSize = 300f;
        int row = positionIndex % 3; 
        int column = positionIndex / 3;
        float offsetX = column * shopItemSize;
        float offsetY = -row * shopItemSize;
        shopItemRectTransform.anchoredPosition = new Vector2 (offsetX, offsetY);

        // Text & Image setup
        shopButton.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopButton.Find("GoldText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopButton.Find("ItemIcon").GetComponent<Image>().sprite = itemSprite;
    }
}
