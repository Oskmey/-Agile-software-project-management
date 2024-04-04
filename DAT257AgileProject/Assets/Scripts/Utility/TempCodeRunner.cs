using UnityEngine;

// This is a temporary script to test the Trash module
// before the rest of the game is implemented,
// can most likely be deleted during the merge. 
public class TempCodeRunner : MonoBehaviour
{
    private TrashScript trashScript;
    private GameplayHudHandler gameplayHudHandler;

    private void Start()
    {
        gameplayHudHandler = FindObjectOfType<GameplayHudHandler>();
        GameObject trash = TrashFactory.CreateTrash(TrashType.TrashBag);
        trash.transform.position = new Vector2(-2, -2);
        trashScript = trash.GetComponent<TrashScript>();
        gameplayHudHandler.SetTrashInfoPanel(trashScript);
    }
}