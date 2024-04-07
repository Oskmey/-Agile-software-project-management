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
        infoPanelHandler.SetTrashTypeText(trash.TrashType);
        // TODO: Add method to randomly select a fact from the list
        infoPanelHandler.SetTrashInformationText("fix");
        infoPanelHandler.SetTrashMoneyValueText(trash.MoneyValue);
        infoPanelHandler.SetTrashRarityText(trash.Rarity);
    }
}
