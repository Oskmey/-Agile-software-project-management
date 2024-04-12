using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float maxSpeed;
    private Rigidbody2D rb;
    private InputAction movementAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["movement"];
        maxSpeed = new Vector2(speed, speed).magnitude;
    }

    private void OnEnable()
    {
        movementAction.performed += OnMovment;
        movementAction.canceled += OnMovmentStopped;
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMovment;
        movementAction.canceled -= OnMovmentStopped;
    }

    private void OnMovment(InputAction.CallbackContext value)
    {
        rb.velocity = value.ReadValue<Vector2>() * speed;
    }

    private void OnMovmentStopped(InputAction.CallbackContext value)
    {
        Debug.Log("Movement stopped");
        rb.velocity = Vector2.zero;
    }
}
