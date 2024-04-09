using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayHudHandler : MonoBehaviour
{
    private InfoPanelHandler infoPanelHandler;

    private void Start()
    {
        infoPanelHandler = GetComponentInChildren<InfoPanelHandler>();
        infoPanelHandler.gameObject.SetActive(false);
    }

    public void ShowTrashInfoHandler(TrashScript trash)
    {
        SetTrashInfoPanel(trash);
        infoPanelHandler.gameObject.SetActive(true);
    }

    public void HideTrashInfoHandler()
    {
        infoPanelHandler.gameObject.SetActive(false);
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
