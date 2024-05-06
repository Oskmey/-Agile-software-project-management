using Inventory;
using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameplayHudHandler : MonoBehaviour
{
    private InfoPanelHandler infoPanelHandler;
    private PlayerStatsManager playerStatsManager;
    [SerializeField]
    private TextMeshProUGUI moneyGeneratedText;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private WarningPopup warningPopup;
    [SerializeField]
    private ShopPlayer shopPlayer;
    [SerializeField]
    private InventoryManager inventoryManager;

    public event Action<bool> OnInfoPopupActive;

    private void Start()
    {
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        infoPanelHandler = GetComponentInChildren<InfoPanelHandler>();
        infoPanelHandler.gameObject.SetActive(false);

        if (shopPlayer != null)
        {
            shopPlayer.OnBuyNoFreeInventorySlot += () => UpdateWarningPopup("Your purchase was unsuccessful, inventory is full");
            shopPlayer.OnBuyNotEnoughMoney += () => UpdateWarningPopup("Your purchase was unsuccessful, not enough money");
        }

        if (inventoryManager != null)
        {
            inventoryManager.SwapItemToAccessoryIncorrect += () => UpdateWarningPopup("Must equip valid items");
        }
    }

    void Update()
    {
        UpdateMoneyGenerated();
    }

    void OnDestroy()
    {
        if(shopPlayer != null)
        {
            shopPlayer.OnBuyNoFreeInventorySlot -= () => UpdateWarningPopup("Your purchase was unsuccessful, inventory is full");
            shopPlayer.OnBuyNotEnoughMoney -= () => UpdateWarningPopup("Your purchase was unsuccessful, not enough money");
        }

        if (inventoryManager != null)
        {
            inventoryManager.SwapItemToAccessoryIncorrect -= () => UpdateWarningPopup("Must equip valid items");
        }
    }

    public void UpdateWarningPopup(string warning)
    {
        // if inventory is full
        // fishing while inventory is full
        // while shopping
        if (warningPopup != null)
        {
            warningPopup.DisplayWarning(warning);
        }
    }

    void UpdateMoneyGenerated()
    {
        if (moneyGeneratedText != null)
        {
            moneyGeneratedText.text = $"Money: {playerStatsManager.Money}";
        }
    }

    public void ShowTrashInfoHandler(TrashScript trash)
    {
        SetTrashInfoPanel(trash);
        infoPanelHandler.gameObject.SetActive(true);
        OnInfoPopupActive?.Invoke(true);
    }

    public void HideTrashInfoHandler()
    {
        infoPanelHandler.gameObject.SetActive(false);
        OnInfoPopupActive?.Invoke(false);
    }

    private void SetTrashInfoPanel(TrashScript trash)
    {
        if (infoPanelHandler != null)
        {
            TrashFactData randomTrashFact = trash.GetRandomTrashFact();

            infoPanelHandler.SetTrashTypeText(trash.TrashType);
            infoPanelHandler.SetTrashInformationText(randomTrashFact.TrashFact);
            infoPanelHandler.SetTrashMoneyValueText(trash.MoneyValue);
            infoPanelHandler.SetTrashRarityText(trash.Rarity);
            infoPanelHandler.SetTrashSourcesText(randomTrashFact.SourcesInformation);
        } 
        else
        {
            Debug.LogError("InfoPanelHandler is null");
        }
    }

    // Methods for testing
    public void TryFindInfoPanel()
    {
        do
        {
            infoPanelHandler = GetComponentInChildren<InfoPanelHandler>();
        } while (infoPanelHandler == null);
    }
}
