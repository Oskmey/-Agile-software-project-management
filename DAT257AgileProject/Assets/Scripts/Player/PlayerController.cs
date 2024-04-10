using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycle;

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
    // Test method to recycle trash due to no inventory system
    private void RecycleAtNearestMachine(InputAction.CallbackContext context)
    {
        if(recyclingManager.TrashToRecycle.Count > 0)
        {
            //recyclingManager.RecycleAtNearestMachine(recyclingManager.TrashToRecycle.ToList()[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
