using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryManager : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private PlayerController playerController;

    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        playerController = FindAnyObjectByType<PlayerController>();
        InitAccessoryEffects();
    }

    void Update()
    {
        //if(playerController != null)
        //{
           // Debug.Log("Player speed: " + playerController.speed);
        //}
    }

    private void InitAccessoryEffects()
    {
        inventoryManager.AccessoryEquipped += ApplyEffects;
        inventoryManager.AccessoryUnEquipped += UnApplyEffects;
    }
    
    private void UnApplyEffects(AccessorySO accessory)
    {
        if(playerController != null)
        {
            foreach (EffectSO ae in accessory.accessoryEffects)
            {
                ae.UnApplyEffect();
            }
        }
    }

    private void ApplyEffects(AccessorySO accessory)
    {
        if (playerController != null)
        {
            foreach (EffectSO ae in accessory.accessoryEffects)
            {
                ae.ApplyEffect();
            }
        }
    }
}