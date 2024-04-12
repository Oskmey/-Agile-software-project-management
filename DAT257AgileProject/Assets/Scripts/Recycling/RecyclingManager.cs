using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RecyclingMachine;

public class RecyclingManager : MonoBehaviour
{
    private IReadOnlyList<RecyclingMachine> recyclingMachines;
    private PlayerStatsManager playerStatsManager;
    // TEMP: list to store the trash that the player has to recycle
    private List<TrashScript> trashToRecycle;
    private bool trashWasRecycled;
    public IReadOnlyList<TrashScript> TrashToRecycle
    {
        get
        {
            return trashToRecycle;
        }
    }
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
        trashToRecycle = new();
        trashWasRecycled = false;
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
    }

    public void RecycleAtNearestMachine()
    {
        if (TrashToRecycle.Count > 0)
        {
            RecycleAtNearestMachine(TrashToRecycle[0]);
        } 
    }

    private void RecycleAtNearestMachine(TrashScript trash)
    {
        foreach (RecyclingMachine recyclingMachine in recyclingMachines)
        {
            // TODO: Make it so player can only recycle trash to nearest recycling machine
            // if (recyclingMachine.IsPlayerInRange(player.transform.position))
            {
                // NOTE: Trash is not recyclable by default, needs to be RecycableTrash

                if (trash.IsRecyclable)
                {
                    Debug.Log("Trash was recycled");
                    playerStatsManager.Money += trash.MoneyValue;
                    playerStatsManager.RecycledTrashList.Add(trash);
                    trashToRecycle.Remove(trash);
                    trashWasRecycled = true;
                }
                else
                {
                    trashWasRecycled = false;
                }
            }
            // else
            // {
            // Debug.Log("Player is not in range of recycling machine");
            // }
        }
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

    public void AddTrashToRecycle(TrashScript trash)
    {
        trashToRecycle.Add(trash);
    }
}
