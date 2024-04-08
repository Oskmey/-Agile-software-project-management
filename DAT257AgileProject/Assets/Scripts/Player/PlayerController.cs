using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycle;

    public PlayerInputActions PlayerControls
    {
        get
        {
            return playerControls;
        }
    }

    public InputAction Recycle
    {
        get
        {
            return recycle;
        }
    }

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
        GameObject trash = new();
        recyclingManager.RecycleAtNearestMachine(trash);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
