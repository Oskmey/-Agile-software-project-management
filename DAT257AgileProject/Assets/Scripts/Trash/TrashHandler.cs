using UnityEngine;
using UnityEngine.InputSystem;

public class TrashHandler : MonoBehaviour
{
    private GameObject currentTrashObject;
    private GameplayHudHandler gameplayHudHandler;
    private PlayerInput playerInput;
    private InputAction hideTrashInfoPanelAction;
    private RecyclingManager recyclingManager;

    private void Start()
    {
        gameplayHudHandler = FindObjectOfType<GameplayHudHandler>();
        playerInput = GetComponent<PlayerInput>();
        hideTrashInfoPanelAction = playerInput.actions["HideTrashInfoPanel"];
        recyclingManager = FindObjectOfType<RecyclingManager>();
    }

    private void Update()
    {
        if (hideTrashInfoPanelAction.triggered)
        {
            DestroyTrash();
        }
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
            Destroy(currentTrashObject);
        }
    }

    public GameObject CurrentTrashObject => currentTrashObject;

    // Methods for testing
    // FindObjectOfType and similar methods don't work as expected in tests
    // But will work if you add a delay before calling them
    // I think the loop is unnecessary, but it's there to make sure the object is found
    public void TryFindGameplayHudHandler()
    {
        do
        {
            gameplayHudHandler = FindObjectOfType<GameplayHudHandler>();
        } while (gameplayHudHandler == null);
    }
}
