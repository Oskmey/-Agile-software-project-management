using Codice.Client.BaseCommands.Merge.Xml;
using System.Collections;
using System.Collections.Generic;
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
    void Awake()
    {
        container = transform.Find("Container");
        shopPlayer = GameObject.FindGameObjectWithTag("Shop Player").GetComponent<ShopPlayer>();
    }

    private void Start()
    {
        CreateShopButton(PurchasableItem.Type.Egg1);
        CreateShopButton(PurchasableItem.Type.Egg2);
        CreateShopButton(PurchasableItem.Type.Egg2);
        CreateShopButton(PurchasableItem.Type.Egg1);
        CreateShopButton(PurchasableItem.Type.Egg1);
        CreateShopButton(PurchasableItem.Type.Egg2);
        CreateShopButton(PurchasableItem.Type.Egg1);
        CreateShopButton(PurchasableItem.Type.Egg2);
    }

    private void CreateShopButton(PurchasableItem.Type type)
    {
        Transform shopButton = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopButton.GetComponent<RectTransform>();

        // Text & Image setup
        string itemName = PurchasableItem.GetName(type);
        int itemCost = PurchasableItem.GetCost(type);
        Sprite itemSprite = PurchasableItem.GetSprite(type);

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

}
