using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Inventory;
using Inventory.Model;
using PlasticGui.WorkspaceWindow;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class MapInteraction : Ainteractable
{

    private Transform container;

    [SerializeField]
    private Transform worldTemplate;
    public override string text => "Press E to open map";
    private GameObject ui;
    private Inventory.Model.InventorySO inventoryData;
    public override void interact()
    {
        ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        container = GameObject.FindGameObjectWithTag("Content").GetComponent<RectTransform>();
        Debug.Log(container);
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
                    Debug.Log(item.Item.Name);
                    if(item.Item.Name == "Savannah Travel Object"){
                        
                        Transform worldButton = Instantiate(worldTemplate, container);
                        RectTransform shopItemRectTransform = worldButton.GetComponent<RectTransform>();
                        worldButton.Find("NameText").GetComponent<TextMeshProUGUI>().SetText("Savannah World");
                        //worldButton.Find("ItemIcon").GetComponent<Image>().sprite = type.sprite;
                        //Image border = worldButton.GetComponent<Image>();
                    }
                    Debug.Log("Map found");

                }
            }
        }
    }

}
