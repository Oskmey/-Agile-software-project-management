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

    private PlayerInteraction playerInteraction;

    private FishingSpot fishingLoop;
    private PlayerStatsManager playerStatsManager;

    private void Start()
    {
        playerInteraction = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInteraction>();
        gameplayHudHandler = GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<GameplayHudHandler>();
        playerInput = GetComponent<PlayerInput>();
        hideTrashInfoPanelAction = playerInput.actions["HideTrashInfoPanel"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();

        // Kommer alltid vara null vid start
        // fishingLoop = playerInteraction.currentFishingSpot;
        // if (fishingLoop != null)
        // {
        //     OnTrashCollected += fishingLoop.ResetFishingLoop;
        // }
    }

    private void Update()
    {
         if (hideTrashInfoPanelAction.triggered)
         {
            DestroyTrash();
            //Reset the loop here instead, does reset the fishingloop twice if u walk away from fishingspot since that also triggers ResetFishingLoop, doesnt really matter tho.
            try
            {
                playerInteraction.currentFishingSpot.ResetFishingLoop();
            }
            catch(Exception e){

            }
         }
    }

    void OnDestroy()
    {
        if(fishingLoop != null)
        {
            OnTrashCollected -= fishingLoop.ResetFishingLoop;
        }
    }

    // Method to trigger the events
    private void TrashCollected()
    {   
            OnTrashCollected?.Invoke();
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

    public void DestroyTrash()
    {
        if (Time.timeScale > 0)
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
                TrashScript trash = currentTrashObject.GetComponent<TrashScript>();
                playerStatsManager.FishedTrash.Add(trash);

                TrashCollected();
                Destroy(currentTrashObject);
            }
  
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
