using Codice.Client.BaseCommands.Merge.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Transform container;
    [SerializeField]
    private Transform shopItemTemplate;
    [SerializeField]
    private string worldSceneName = "First World";

    [SerializeField]
    private ShopPlayer shopPlayer;

    [Header("Shop Items based on time of the day")]
    [SerializeField]
    private int minItems;
    [SerializeField]
    private int midItems;
    [SerializeField]
    private int maxItems;

    [Header("Shop Items that the player can buy in this shop")]
    [SerializeField]
    private List<AccessorySO> shopingItems;

    void Awake()
    {
        container = transform.Find("Container");
        shopPlayer = GameObject.FindGameObjectWithTag("Shop Player").GetComponent<ShopPlayer>();
        GenerateShopingBasedOnTimeItems();
    }

    private void CreateShopButton(AccessorySO type)
    {
        Transform shopButton = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopButton.GetComponent<RectTransform>();

        // Text & Image setup
        string itemName = type.accessoryName;
        int itemCost = type.cost;
        Sprite itemSprite = type.sprite;

        shopButton.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopButton.Find("GoldText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopButton.Find("ItemIcon").GetComponent<Image>().sprite = itemSprite;

        // Add onClick listener to the button
        Button buttonComponent = shopButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => shopPlayer.TryToBuy(type));
        }
    }

    public void ExitShop()
    {
        SceneManager.LoadScene(worldSceneName);
    }


    private void GenerateShopingBasedOnTimeItems()
    {
        int ShopItems = minItems;
        DateTime computerTime = DateTime.Now;
        int hour = computerTime.Hour;
        if (hour % 6 == 0)
        {
            ShopItems = maxItems;
        }
        else if (hour % 3 == 0)
        {
            ShopItems = midItems;
        }
        for (int i = 0; i < ShopItems; i++)
        {
            GenerateShopingItems(new System.Random(hour + i));
        }
    }

    private void GenerateShopingItems(System.Random hourSeed)
    {
        int rng = hourSeed.Next(0, 100);
        List<AccessorySO> items = shopingItems.Where(item => rng <= (int)item.rarity).ToList();// This takes the rarity of the item and compares it to the random number generated
        if (items.Any())
        {
            AccessorySO item = items[hourSeed.Next(0, items.Count)];
            CreateShopButton(item);
        }
        else
        {
            CreateShopButton(shopingItems[0]);
        }

    }   
}