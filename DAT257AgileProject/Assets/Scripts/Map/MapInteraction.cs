
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

    [SerializeField]
    private Transform worldTemplate;
    public override string text => "Press E to open map";
    private GameObject ui;
    private Inventory.Model.InventorySO inventoryData;
    public override void interact()
    {
        ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        container = ui.transform.Find("Scroll View").Find("Viewport").Find("Content").transform;
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
                    Debug.Log(container);
                    Transform world = Instantiate(worldTemplate, container);
                    world.Find("NameText").GetComponent<TextMeshProUGUI>().SetText(mapItemSO.MapName);
                    world.Find("ItemIcon").GetComponent<Image>().sprite = mapItemSO.MapSprite;
                    instantiatedTemplates.Add(world);
                    world.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        LoadMap(mapItemSO.SceneName);
                    });
                }
            }
        }
        Transform defaultWorld = Instantiate(worldTemplate, container);
        defaultWorld.Find("NameText").GetComponent<TextMeshProUGUI>().SetText("Grassy grove");
        defaultWorld.Find("ItemIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Assets/Sprites/Map_sprites/Image_world_1");
        instantiatedTemplates.Add(defaultWorld);
        defaultWorld.GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadMap("First World");
        });
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
