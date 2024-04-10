using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction fishAction;
    private InputAction recycleAction;
    private TrashHandler trashHandler;
    private RecyclingManager recyclingManager;
    private TrashScript lastFishedTrash;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        fishAction = playerInput.actions["Fish"];
        recycleAction = playerInput.actions["Recycle"];
        trashHandler = FindObjectOfType<TrashHandler>();
        recyclingManager = FindObjectOfType<RecyclingManager>();
    }

    void Update()
    {
        if (fishAction.triggered)
        {
            // Trash spawns at player location with an offset
            Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y + 1);
            trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);
            lastFishedTrash = trashHandler.CurrentTrashObject.GetComponent<TrashScript>();
        }
        else if (recycleAction.triggered)
        {
            recyclingManager.RecycleAtNearestMachine(lastFishedTrash);
        }
    }
}
