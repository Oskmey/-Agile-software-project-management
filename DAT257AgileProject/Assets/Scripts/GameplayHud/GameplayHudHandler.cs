using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayHudHandler : MonoBehaviour
{
    private InfoPanelHandler infoPanelHandler;
    private PlayerInput UIPlayerInput;
    // Not necessary to have, but is nice to have.
    private InputAction hideTrashInfoPanelAction;

    private void Start()
    {
        infoPanelHandler = FindObjectOfType<InfoPanelHandler>();
        infoPanelHandler.gameObject.SetActive(false);
        UIPlayerInput = FindObjectOfType<PlayerInput>();
        hideTrashInfoPanelAction = UIPlayerInput.actions["HideTrashInfoPanel"];
    }

    private void Update()
    {
        if (hideTrashInfoPanelAction.triggered)
        {
            HideTrashInfoHandler();
        }
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
}
