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
        infoPanelHandler.SetTrashNameText(trash.TrashType.ToReadableString());
        infoPanelHandler.SetTrashDescriptionText(trash.TrashInformation);
        infoPanelHandler.SetTrashMoneyValueText(trash.MoneyValue.ToString());
        infoPanelHandler.SetTrashRarityText(trash.Rarity.ToString());
    }
}
