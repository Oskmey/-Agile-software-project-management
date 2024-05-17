using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapInteraction : Ainteractable
{
    [SerializeField]
    private Transform worldTemplate;

    [SerializeField]
    private mapItemSO defaultMapItem;

    private Transform container;
    private List<Transform> instantiatedTemplates = new List<Transform>();
    public override string text => "Press E to open map";
    private GameObject ui;
    private List<mapItemSO> purchasedMaps;

    public override void Interact()
    {
        GameObject gameplayHUD = GameObject.Find("GameplayHUD");
        Transform mapSelectionUI = gameplayHUD.transform.Find("MapselectionUI");
        Transform scrollView = mapSelectionUI.transform.Find("Scroll View");
        Transform viewport = scrollView.transform.Find("Viewport");
        Transform content = viewport.transform.Find("Content");

        ui = mapSelectionUI.gameObject;
        container = content.transform;

        if (ui != null && !ui.activeSelf)
        {
            ui.SetActive(true);
            GetPlayerMaps();
        }
        else if (ui != null && ui.activeSelf)
        {
            ui.SetActive(false);
            ClearMapSelection();
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
            itemIcon.sprite = defaultMapItem.MapSprite;
        }

        if (mapItemSO.MapName != null)
        {
            nameText.SetText(mapItemSO.MapName);
        }
        else
        {
            Debug.LogWarning("Missing map item name");
            nameText.SetText(defaultMapItem.MapName);
        }

        button.onClick.AddListener(() =>
        {
            PlayerExitEvent();
            LoadMap(mapItemSO.SceneName);
        });

        instantiatedTemplates.Add(world);
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