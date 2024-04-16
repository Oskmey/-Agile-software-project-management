using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static RecyclingMachine;

public class TrashHandler : MonoBehaviour
{
    private GameObject currentTrashObject;
    [SerializeField]
    private GameplayHudHandler gameplayHudHandler;
    private PlayerInput playerInput;
    private InputAction hideTrashInfoPanelAction;
    private RecyclingManager recyclingManager;

    private UnityEvent onTrashCollected;

    private void Start()
    {
        onTrashCollected = new UnityEvent();
        //onTrashCollected.AddListener(FindObjectOfType<FishingLoop>().ResetFishingLoop);
        gameplayHudHandler = GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<GameplayHudHandler>();
        playerInput = GetComponent<PlayerInput>();
        //hideTrashInfoPanelAction = playerInput.actions["HideTrashInfoPanel"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    private void Update()
    {
        // if (hideTrashInfoPanelAction.triggered)
        // {
        //     DestroyTrash();
        // }
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
        // TODO: Fix gameplayHudHandler being null, so we dont have to find it again, something to do with event invoke

        gameplayHudHandler = GameObject.FindGameObjectWithTag("GameplayHUD").GetComponent<GameplayHudHandler>();

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

    public void CreateRandomTrash(TrashRarity trashRarity, Vector2 position)
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
            TrashScript trash = currentTrashObject.GetComponent<TrashScript>();
            recyclingManager.AddTrashToRecycle(trash);

            onTrashCollected.Invoke();
            Destroy(currentTrashObject);
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
