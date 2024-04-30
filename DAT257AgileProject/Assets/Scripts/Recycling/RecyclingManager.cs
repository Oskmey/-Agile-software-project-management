using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static RecyclingMachine;

public class RecyclingManager : MonoBehaviour
{
    [SerializeField]
    private InventorySO playerInventory;

    private IReadOnlyList<RecyclingMachine> recyclingMachines;
    private PlayerStatsManager playerStatsManager;
    private bool trashWasRecycled;

    public IReadOnlyList<RecyclingMachine> RecyclingMachines
    {
        get
        {
            return recyclingMachines;
        }
    }

    public bool TrashWasRecycled
    {
        get
        {
            return trashWasRecycled;
        }
    }

    void Awake()
    {
        trashWasRecycled = false;
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
    }

    public void RecycleAtNearestMachine()
    {
        int money = playerStatsManager.Money;
        foreach (RecyclingMachine recyclingMachine in recyclingMachines)
        {
            if (recyclingMachine.IsPlayerInRange())
            {
                List<TrashData> trashToRecycle = playerInventory.GetAndRemoveRecyclableTrashItems();

                foreach (TrashData trash in trashToRecycle)
                {
                    money += trash.MoneyValue;
                }
            }
        }
        playerStatsManager.Money += money;
    }

    public IReadOnlyList<RecyclingMachine> GetRecyclingMachines()
    {
        List<RecyclingMachine> recyclingMachines = new();
        GameObject[] recycleMachines = GameObject.FindGameObjectsWithTag("Recycle Machine");

        foreach (GameObject recyclingMachine in recycleMachines)
        {
            recyclingMachine.GetComponent<RecyclingMachine>();
            recyclingMachines.Add(recyclingMachine.GetComponent<RecyclingMachine>());
        }

        return recyclingMachines;
    }
}