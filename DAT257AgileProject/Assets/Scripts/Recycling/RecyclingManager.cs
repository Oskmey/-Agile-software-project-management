using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RecyclingMachine;

public class RecyclingManager : MonoBehaviour
{
    private IReadOnlyList<RecyclingMachine> recyclingMachines;
    private PlayerStatsManager playerStatsManager;
    private bool trashWasRecycled;
    private TrashHandler TrashHandler;

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
        TrashHandler = FindObjectOfType<TrashHandler>().GetComponent<TrashHandler>();
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
    }

    public void RecycleAtNearestMachine()
    {
        if (playerStatsManager.FishedTrash.Count > 0)
        {
            RecycleAtNearestMachine(playerStatsManager.FishedTrash[0]);
        }
        else
        {
            Debug.Log("No trash to recycle");
        }
    }

    private void RecycleAtNearestMachine(TrashType trashType)
    {
        TrashScript trash = GetTrashScriptFromType(trashType);
        foreach (RecyclingMachine recyclingMachine in recyclingMachines)
        {
            // TODO: Make it so player can only recycle trash to nearest recycling machine
            // if (recyclingMachine.IsPlayerInRange(player.transform.position))
            if (recyclingMachine.IsPlayerInRange())
            {
                // NOTE: Trash is not recyclable by default, needs to be RecycableTrash
                if (trash.IsRecyclable)
                {
                    Debug.Log(trash.MoneyValue);
                    playerStatsManager.Money += trash.MoneyValue;
                    Debug.Log(playerStatsManager.Money);
                    UpdateTrashDictionary(trash);
                    playerStatsManager.FishedTrash.Remove(trashType);
                    trashWasRecycled = true;
                    TrashHandler.DestroyTrash();
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

    // It is not possible to save scripts between sessions, so here
    // is a method to convert the saved TrashTypes to TrashScripts by
    // creating them using the factory, this will not affect other things, 
    // which would have happened if trashHandler's create trash was used instead. 
    private static TrashScript GetTrashScriptFromType(TrashType trashType)
    {
        GameObject trashObject = TrashFactory.CreateTrash(trashType);
        TrashScript trash = trashObject.GetComponent<TrashScript>();
        Destroy(trashObject);
        return trash;
    }

    private void UpdateTrashDictionary(TrashScript trash)
    {
        if (playerStatsManager.RecycledTrashDictionary.ContainsKey(trash.TrashType))
        {
            playerStatsManager.RecycledTrashDictionary[trash.TrashType]++;
        }
        else
        {
            playerStatsManager.RecycledTrashDictionary.Add(trash.TrashType, 1);
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
}
