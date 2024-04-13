using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private InputAction movementAction;
    private PlayerInput playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycleAction;

    private InputAction fishingAction;
    private FishingManager fishingManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["Movement"];

        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        
        fishingAction = GetComponent<PlayerInput>().actions["Fish"];
        fishingManager = GameObject.FindGameObjectWithTag("Fishing Manager").GetComponent<FishingManager>();
    }

    private void OnEnable()
    {
        movementAction.performed += OnMovment;
        movementAction.canceled += OnMovmentStopped;
        recycleAction.performed += Recycle;
        fishingAction.performed += Fishing;
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMovment;
        movementAction.canceled -= OnMovmentStopped;
        recycleAction.performed -= Recycle;
    }
    
    // Test method to recycle trash due to no inventory system
    private void Recycle(InputAction.CallbackContext context)
    {
        Debug.Log("Recycling trash if in range");
        recyclingManager.RecycleAtNearestMachine();
    }

    private void Fishing(InputAction.CallbackContext context)
    {
        Debug.Log("Fishing if in range");
        fishingManager.FishAtNearestSpot();
    }

    private void OnMovment(InputAction.CallbackContext value)
    {
        rb.velocity = value.ReadValue<Vector2>() * speed;
    }

    private void OnMovmentStopped(InputAction.CallbackContext value)
    {
        rb.velocity = Vector2.zero;
    }
}
