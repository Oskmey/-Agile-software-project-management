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
    private string worldSceneName = "First World";
    private Transform container;
    [SerializeField]
    private Transform shopItemTemplate;

    [SerializeField]
    private ShopPlayer shopPlayer;
    [Header("Rarity Borders for the shop items")]
    [SerializeField]
    private Sprite commonBorder;
    [SerializeField]
    private Sprite uncommonBorder;
    [SerializeField]
    private Sprite rareBorder;
    [SerializeField]
    private Sprite epicBorder;
    [SerializeField]
    private Sprite legendaryBorder;
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


    public string WorldSceneName
    {
        get { return worldSceneName; }
        set
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == value)
                {
                    worldSceneName = value;
                    return;
                }
            }
            Debug.LogError("Scene with name " + value + " not found");
        }
    }

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
        shopButton.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(type.AccessoryName);
        shopButton.Find("GoldText").GetComponent<TextMeshProUGUI>().SetText(type.cost.ToString());
        shopButton.Find("ItemIcon").GetComponent<Image>().sprite = type.sprite;
        Image border = shopButton.GetComponent<Image>();

        switch (type.rarity)
        {
            case AccessoryRarity.Common:
                border.sprite = commonBorder;
                break;
            case AccessoryRarity.Uncommon:
                border.sprite = uncommonBorder;
                break;
            case AccessoryRarity.Rare:
                border.sprite = rareBorder;
                break;
            case AccessoryRarity.Epic:
                border.sprite = epicBorder;
                break;
            case AccessoryRarity.Legendary:
                border.sprite = legendaryBorder;
                break;
            default:
                border.sprite = commonBorder;
                break;
        }


        Button buttonComponent = shopButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => shopPlayer.TryToBuy(type));
        }
    }


    public void ExitShop()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(worldSceneName);
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