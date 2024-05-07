using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryManager : MonoBehaviour
{
    InventoryManager inventoryManager;
    PlayerController playerController;
    List<AccessorySO> acc;

    int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        acc = inventoryManager.GetCurrentlyEquippedAccessories();
        if (acc.Count != itemCount)
        {
            ApplyEffect();
            itemCount = acc.Count;
        }
    }

    void ApplyEffect()
    {
        int speedEggs = 0;

        foreach (AccessorySO egg in acc)
        {
            // Temp solution for SpeedEffect
            if (egg.AccessoryName == "Egg")
            {
                speedEggs++;
            }
        }

        playerController.speed = 5f + speedEggs;
    }
}
