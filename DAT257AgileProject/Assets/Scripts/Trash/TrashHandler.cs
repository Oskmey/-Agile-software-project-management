using Inventory.Model;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Minigame;
using static RecyclingMachine;

public class TrashHandler : MonoBehaviour
{
    private GameObject currentTrashObject;
    [SerializeField]
    private GameplayHudHandler gameplayHudHandler;
    private PlayerInput playerInput;
    private InputAction hideTrashInfoPanelAction;
    private RecyclingManager recyclingManager;

    private delegate void TrashEvent();
    private event TrashEvent OnTrashCollected;
    private event TrashEvent OnTrashCollectedAndInventoryFull;

    private PlayerInteraction playerInteraction;

    private FishingSpot fishingLoop;

    [SerializeField]
    private InventorySO inventoryData;

    private bool infoPopupActive = false;
    private PlayerStatsManager playerStatsManager;

    private void Start()
    {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInteraction>();
        gameplayHudHandler = GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<GameplayHudHandler>();
        playerInput = GetComponent<PlayerInput>();
        hideTrashInfoPanelAction = playerInput.actions["HideTrashInfoPanel"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        OnTrashCollectedAndInventoryFull += () => gameplayHudHandler.UpdateWarningPopup("Can't collect the trash while there is no available slot in your inventory");
        gameplayHudHandler.OnInfoPopupActive += SetInfoPopupActive;
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();

        // fishingLoop = playerInteraction.currentFishingSpot;
        // if (fishingLoop != null)
        // {
        //     OnTrashCollected += fishingLoop.ResetFishingLoop;
        // }
    }

    private void SetInfoPopupActive(bool isActive)
    {
        infoPopupActive = isActive;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            HandleFishingLoopReset();
        }
    }

    private void HandleFishingLoopReset()
    {
        if (hideTrashInfoPanelAction.triggered && infoPopupActive)
        {
            DestroyTrash();
            //Reset the loop here instead, does reset the fishingloop twice if u walk away from fishingspot since that also triggers ResetFishingLoop, doesnt really matter tho.
            try
            {
                playerInteraction.currentFishingSpot.ResetFishingLoop();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"The following Exception occurred: {e}");
            }
        }
    }

    void OnDestroy()
    {
        OnTrashCollectedAndInventoryFull -= () => gameplayHudHandler.UpdateWarningPopup("Can't collect the trash while there is no available slot in your inventory");
        if (fishingLoop != null)
        {
            OnTrashCollected -= fishingLoop.ResetFishingLoop;
        }
    }

    // Method to trigger the events
    private void TrashCollected()
    {   
        OnTrashCollected?.Invoke();
    }

    private void TrashCollectedWhileFullInventory()
    {
        OnTrashCollectedAndInventoryFull?.Invoke();
    }

    // Creates trash at the center of the screen
    public void CreateTrash(TrashType trashType)
    {
        CreateTrash(trashType, new Vector2(0, 0));
    }

    // Creates trash at the specified position
    public void CreateTrash(TrashType trashType, Vector2 position)
    {
        currentTrashObject = TrashFactory.CreateTrash(trashType);
        currentTrashObject.transform.position = position;
        TrashScript currentTrashScript = currentTrashObject.GetComponent<TrashScript>();

        if (gameplayHudHandler != null)
        {
            gameplayHudHandler.ShowTrashInfoHandler(currentTrashScript);
            TrashScript trash = currentTrashObject.GetComponent<TrashScript>();
        } 
        else
        {
            Debug.LogError("GameplayHudHandler not found.");
        }
    }

    public TrashScript CreateRandomTrash(TrashRarity trashRarity, Vector2 position)
    {
        currentTrashObject = TrashFactory.CreateRandomTrashBasedOnRarity(trashRarity);
        currentTrashObject.transform.position = position;
        TrashScript currentTrashScript = currentTrashObject.GetComponent<TrashScript>();

        gameplayHudHandler = GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<GameplayHudHandler>();

        if (gameplayHudHandler != null)
        {
            gameplayHudHandler.ShowTrashInfoHandler(currentTrashScript);
        }
        else
        {
            Debug.LogError("GameplayHudHandler not found.");
        }
        return currentTrashScript;
    }
    private void UpdateRecycledTrashDictionary(TrashType trashType)
    {
        if (playerStatsManager.RecycledTrashDictionary.ContainsKey(trashType))
        {
            playerStatsManager.RecycledTrashDictionary[trashType]++;
        }
        else
        {
            playerStatsManager.RecycledTrashDictionary.Add(trashType, 1);
        }
    }
    public void DestroyTrash()
    {
        if (gameplayHudHandler != null)
        {
            gameplayHudHandler.HideTrashInfoHandler();
        }
        else
        {
            Debug.LogError("GameplayHudHandler not found.");
        }

        if (currentTrashObject != null)
        {
            TrashCollected();

            TrashType trashType = currentTrashObject.GetComponent<TrashScript>().TrashType;
            if (playerStatsManager.TrashCaughtDictionary.ContainsKey(trashType))
            {
                playerStatsManager.TrashCaughtDictionary[trashType]++;
            }
            else
            {
                playerStatsManager.TrashCaughtDictionary.Add(trashType, 1);
            }

            if (currentTrashObject.TryGetComponent<Item>(out var item))
            {
                int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (remainder == 0)
                {
                    item.CollectItem();
                }
                else
                {
                    TrashCollectedWhileFullInventory();
                    item.DestroyItem();
                    item.Quantity = remainder;
                }
            }

            currentTrashObject = null;
        }   
    }

    public GameObject CurrentTrashObject => currentTrashObject;

    // Methods for testing
    // FindObjectOfType and similar methods don't work as expected in tests
    // But will work if you add a delay before calling them
    // I think the loop is unnecessary, but it's there to make sure the object is found
    public void TryFindManagers()
    {
        do
        {
            gameplayHudHandler = FindObjectOfType<GameplayHudHandler>();
        } while (gameplayHudHandler == null);
        do
        {
            recyclingManager = FindObjectOfType<RecyclingManager>();
        } while (recyclingManager == null);
    }
}
