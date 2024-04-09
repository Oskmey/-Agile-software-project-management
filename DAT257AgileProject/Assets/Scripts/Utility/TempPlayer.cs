using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction fishAction;
    private TrashHandler trashHandler;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        fishAction = playerInput.actions["Fish"];
        trashHandler = FindObjectOfType<TrashHandler>();
    }

    void Update()
    {
        if (fishAction.triggered)
        {
            // Trash spawns at player location with an offset
            Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y + 1);
            trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);
        }
    }
}
