
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapInteraction : Ainteractable
{

    private Transform container;
    private List<Transform> instantiatedTemplates = new List<Transform>();
    private bool isMapOpen = false;

    [SerializeField]
    private Transform worldTemplate;
    public override string text => "Press E to open map";
    private GameObject ui;
    private Inventory.Model.InventorySO inventoryData;
    public override void interact()
    {
        ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        container = ui.transform.Find("Scroll View").Find("Viewport").Find("Content").transform;
        if (ui != null && !isMapOpen)
        {
            ui.SetActive(true);
            isMapOpen = true;
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
        isMapOpen = false;
        instantiatedTemplates.ForEach(m => Destroy(m.gameObject));
        instantiatedTemplates.Clear();
    }

    private void getPlayerMaps()
    {
        foreach (InventoryItem item in inventoryData.InventoryItems)
        {
            if (item.Item != null)
            {
                if (item.Item is mapItemSO mapItemSO)
                {
                    makeMapSelectFromItem(mapItemSO);
                }
            }
        }
        makeDefaultWorldSelect();
    }
    private void makeMapSelectFromItem(mapItemSO mapItemSO)
    {
        Transform world = Instantiate(worldTemplate, container);
        TextMeshProUGUI nameText = world.Find("NameText").GetComponent<TextMeshProUGUI>();
        Image itemIcon = world.Find("ItemIcon").GetComponent<Image>();
        Button button = world.GetComponent<Button>();

        
        if (mapItemSO.MapSprite != null)
        {
            itemIcon.sprite = mapItemSO.MapSprite;
        }
        else
        {
            Debug.LogWarning("Missing map item image");
            itemIcon.sprite = Resources.Load<Sprite>("Sprites/Map_sprites/Image_default_map");
        }
        if(mapItemSO.MapName != null)
        {
            nameText.SetText(mapItemSO.MapName);
        }
        else
        {
            Debug.LogWarning("Missing map item name");
            nameText.SetText("Missing Map Name");
        }
        button.onClick.AddListener(() =>
        {
            LoadMap(mapItemSO.SceneName);
        });
        instantiatedTemplates.Add(world);
    }



    private void makeDefaultWorldSelect()
    {
        Transform defaultWorld = Instantiate(worldTemplate, container);
        TextMeshProUGUI nameText = defaultWorld.Find("NameText").GetComponent<TextMeshProUGUI>();
        Image itemIcon = defaultWorld.Find("ItemIcon").GetComponent<Image>();
        Button button = defaultWorld.GetComponent<Button>();

        nameText.SetText("Grassy Grove");
        itemIcon.sprite = Resources.Load<Sprite>("Sprites/Map_sprites/Image_default_world");
        button.onClick.AddListener(() =>
        {
            LoadMap("First World");
        });

        instantiatedTemplates.Add(defaultWorld);
    }

    private void LoadMap(string sceneName)
    {
        if(sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("You are already in this map");
        }

    }


}
