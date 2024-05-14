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
        //Debug.Log(acc.Count);
    }

    void ApplyEffect()
    {
        int speedEggs = 0;

        foreach (AccessorySO acc in acc)
        {
            foreach(AEffect ae in acc.accessoryEffects)
            {
                //ae.Effect
            }
            // Temp solution for SpeedEffect
            if (acc.AccessoryName == "Egg")
            {
                speedEggs++;
            }
        }

        //playerController.speed = 5f + speedEggs;
    }
}
