using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private InputAction recycle;
    private RecyclingManager recyclingManager;

    void Awake()
    {
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        recycle = playerControls.Player.Recycle;
        recycle.Enable();
        recycle.performed += RecycleAtNearestMachine;
    }

    private void OnDisable()
    {
        recycle.Disable();
    }

    private void RecycleAtNearestMachine(InputAction.CallbackContext context)
    {
        List<GameObject> recyclingMachines = recyclingManager.GetRecyclingMachines();

        // TODO: Make it so recycling machines can recycle when near them
        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            // TEMP: Creating trash to recycle
            GameObject trash = new();
            trash.AddComponent<RecyclingMachine.RecycableTrash>();

            recyclingMachine.GetComponent<RecyclingMachine>().Recycle(trash);
        }

        Debug.Log("we are recycling");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
