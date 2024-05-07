using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryManager : MonoBehaviour
{
    InventoryManager inventoryManager;
    List<AccessorySO> acc;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        acc = inventoryManager.GetCurrentlyEquippedAccessories();
        Debug.Log(acc);
    }

    // Update is called once per frame
    void Update()
    {
        acc = inventoryManager.GetCurrentlyEquippedAccessories();
        Debug.Log(acc.Count);
    }
}
