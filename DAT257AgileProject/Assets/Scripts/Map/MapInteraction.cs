
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


    [SerializeField]
    private Transform worldTemplate;

    [SerializeField]
    private Sprite defaultMapSprite;

    private Transform container;
    private List<Transform> instantiatedTemplates = new List<Transform>();
    private bool isMapOpen = false;
    public override string text => "Press E to open map";
    private GameObject ui;
    private List<mapItemSO> purchasedMaps;
    public bool IsMapOpen => isMapOpen;

    public override void Interact()
    {
        ui = GameObject.Find("GameplayHUD").transform.Find("MapselectionUI").gameObject;
        container = ui.transform.Find("Scroll View").Find("Viewport").Find("Content").transform;
        if (ui != null && !isMapOpen)
        {
            ui.SetActive(true);
            isMapOpen = true;
            GetPlayerMaps();
        }
    }

    public void Start()
    {
        purchasedMaps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>().PurchasedMaps;
        PlayerExitHandler += PlayerExitEvent;
    }

    public void Update()
    {
        if (Time.timeScale <= 0 && ui != null)
        {
            ui.SetActive(false);
        }
    }

    private void PlayerExitEvent()
    {
            if (ui != null)
    {
        ui.SetActive(false);
    }
        isMapOpen = false;
        ClearMapSelection();
    }

    private void ClearMapSelection()
    {
        instantiatedTemplates.ForEach(m => Destroy(m.gameObject));
        instantiatedTemplates.Clear();
    }

    private void GetPlayerMaps()
    {
        foreach (mapItemSO item in purchasedMaps)
        {
            MakeMapSelectFromItem(item);
        }
        MakeDefaultWorldSelect();
    }
    private void MakeMapSelectFromItem(mapItemSO mapItemSO)
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
            itemIcon.sprite = defaultMapSprite;
        }
        if (mapItemSO.MapName != null)
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
            PlayerExitEvent();
            LoadMap(mapItemSO.SceneName);
        });
        instantiatedTemplates.Add(world);
    }



    private void MakeDefaultWorldSelect()
    {
        Transform defaultWorld = Instantiate(worldTemplate, container);
        TextMeshProUGUI nameText = defaultWorld.Find("NameText").GetComponent<TextMeshProUGUI>();
        Image itemIcon = defaultWorld.Find("ItemIcon").GetComponent<Image>();
        Button button = defaultWorld.GetComponent<Button>();

        nameText.SetText("Grassy Grove");
        itemIcon.sprite = defaultMapSprite;
        button.onClick.AddListener(() =>
        {
            PlayerExitEvent();
            LoadMap("First World");
        });
        instantiatedTemplates.Add(defaultWorld);
    }

    private void LoadMap(string sceneName)
    {
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            PlayerExitEvent();
            DataPersistenceManager.Instance.SaveGame();
            SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            GameObject.Find("GameplayHUD").transform.Find("WarningPopUp").GetComponent<WarningPopup>().DisplayWarning("You are already in this map");
        }

    }
}
