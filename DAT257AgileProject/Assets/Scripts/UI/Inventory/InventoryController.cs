using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    private int inventorySize = 30;
    private PlayerInput playerInput;
    private InputAction showInventory;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        inventoryUI.InitializeInventoryUI(inventorySize);
        showInventory = playerInput.actions["ShowInventory"];
    }

    public void Update()
    {
        HandleInventoryShow();
    }

    private void HandleInventoryShow()
    {
        if (showInventory.triggered)
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
