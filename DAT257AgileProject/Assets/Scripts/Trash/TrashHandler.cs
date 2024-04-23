using Inventory.Model;
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

    private FishingLoop fishingLoop;

    [SerializeField]
    private InventorySO inventoryData;

    private void Start()
    {
        fishingLoop = FindObjectOfType<FishingLoop>();
        if (fishingLoop != null)
        {
            OnTrashCollected += fishingLoop.ResetFishingLoop;
        }
        gameplayHudHandler = GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<GameplayHudHandler>();
        playerInput = GetComponent<PlayerInput>();
        hideTrashInfoPanelAction = playerInput.actions["HideTrashInfoPanel"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    private void Update()
    {
         if (hideTrashInfoPanelAction.triggered)
         {
             DestroyTrash();
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
                //recyclingManager.AddTrashToRecycle(trash);

                TrashCollected();
                //Destroy(currentTrashObject);

                Item item = currentTrashObject.GetComponent<Item>();

                if (item != null)
                {
                    int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                    if (remainder == 0)
                    {
                        item.DestroyItem();
                    }
                    else
                    {
                        item.Quantity = remainder;
                    }
                }
        }    }
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
