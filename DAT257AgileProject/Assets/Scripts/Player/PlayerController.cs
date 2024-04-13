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
    private RecyclingManager recyclingManager;
    private InputAction recycleAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["Movement"];
        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    private void OnEnable()
    {
        movementAction.performed += OnMovment;
        movementAction.canceled += OnMovmentStopped;
        recycleAction.performed += RecycleAtNearestMachine;
    }

    private void OnDisable()
    {
        movementAction.Disable();
        recycleAction.Disable();
        movementAction.performed -= OnMovment;
        movementAction.canceled -= OnMovmentStopped;
        recycleAction.performed -= RecycleAtNearestMachine;
    }
    
    // Test method to recycle trash due to no inventory system
    private void RecycleAtNearestMachine(InputAction.CallbackContext context)
    {
        if(recyclingManager.TrashToRecycle.Count > 0)
        {
            //recyclingManager.RecycleAtNearestMachine(recyclingManager.TrashToRecycle.ToList()[0]);
        }
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
