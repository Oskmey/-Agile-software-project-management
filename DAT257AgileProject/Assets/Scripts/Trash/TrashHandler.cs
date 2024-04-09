using UnityEngine;
using UnityEngine.InputSystem;

public class TrashHandler : MonoBehaviour
{
    private GameObject currentTrashObject;
    private GameplayHudHandler gameplayHudHandler;
    private PlayerInput playerInput;
    private InputAction hideTrashInfoPanelAction;

    private void Start()
    {
        gameplayHudHandler = FindObjectOfType<GameplayHudHandler>();
        playerInput = GetComponent<PlayerInput>();
        hideTrashInfoPanelAction = playerInput.actions["HideTrashInfoPanel"];
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
        gameplayHudHandler.ShowTrashInfoHandler(currentTrashScript);
    }

    public void DestroyTrash()
    {
        gameplayHudHandler.HideTrashInfoHandler();
        if (currentTrashObject != null)
        {
            Destroy(currentTrashObject);
        }
    }
}
