using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHudHandler : MonoBehaviour
{
    private InfoPanelHandler infoPanelHandler;

    private void Start()
    {
        infoPanelHandler = FindObjectOfType<InfoPanelHandler>();
    }

    public void SetTrashInfoPanel(TrashScript trash)
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
}
