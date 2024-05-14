using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using PlasticGui.WorkspaceWindow;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MapInteraction : Ainteractable
{
    public override string text => "Press E to open map";
    private GameObject ui;
    private Inventory.Model.InventorySO inventoryData;
    public override void interact()
    {
        ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        if (ui != null)
        {
            ui.SetActive(true);
            getPlayerMaps();
        }
    }

    public void Start()
    {
        inventoryData = GameObject.Find("InventoryManager").GetComponent<InventoryManager>().InventoryData;
        PlayerExitHandler += playerExitEvent;
    }

    public void Update()
    {
        if (Time.timeScale <= 0 && ui != null)
        {
            ui.SetActive(false);
        }
    }

    
    private void playerExitEvent()
    {
        ui.SetActive(false);
    }

    private void getPlayerMaps()
    {
        foreach (InventoryItem item in inventoryData.InventoryItems)
        {
            if (item.Item != null)
            {
                if (item.Item is mapItemSO)
                {
                    Debug.Log("Map found");
                }
            }
        }
    }

}
