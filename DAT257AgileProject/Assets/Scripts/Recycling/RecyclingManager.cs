using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RecyclingMachine;

public class RecyclingManager : MonoBehaviour
{
    private List<RecyclingMachine> recyclingMachines = new();
    private PlayerStatsManager playerStatsManager;
    private bool trashWasRecycled;

    public List<RecyclingMachine> RecyclingMachines
    {
        get
        {
            return recyclingMachines;
        }
        set
        {
            recyclingMachines = value;
        }
    }

    public bool TrashWasRecycled
    {
        get
        {
            return trashWasRecycled;
        }
        set
        {
            trashWasRecycled = value;
        }
    }

    void Start()
    {
        TrashWasRecycled = false;
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
    }

    public void RecycleAtNearestMachine(GameObject trash)
    {
        foreach (RecyclingMachine recyclingMachine in recyclingMachines)
        {
            // TODO: Make it so player can only recycle trash to nearest recycling machine
            // if (recyclingMachine.IsPlayerInRange(player.transform.position))
            {
                // TEMP: Creating trash to recycle
                // NOTE: Trash is not recyclable by default, needs to be RecycableTrash
                trash.AddComponent<RecycableTrash>();

                if (recyclingMachine.IsTrashRecyclable(trash)) 
                {
                    recyclingMachine.Recycle(trash);
                    playerStatsManager.Money += trash.GetComponent<RecycableTrash>().trashValue;
                    playerStatsManager.RecycledTrashList.Add(trash);
                    TrashWasRecycled = true;
                }
                else
                {
                    TrashWasRecycled = false;
                    Debug.Log("Not recyclable trash");
                }
            }
            // else
            // {
                // Debug.Log("Player is not in range of recycling machine");
            // }
        }
    }

    public List<RecyclingMachine> GetRecyclingMachines()
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
