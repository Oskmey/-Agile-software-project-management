using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RecyclingMachine;

public class RecyclingManager : MonoBehaviour, IDataPersistence
{
    private IReadOnlyList<RecyclingMachine> recyclingMachines;
    private PlayerStatsManager playerStatsManager;
    // TEMP: list to store the trash that the player has to recycle
    private static List<TrashScript> trashToRecycle;
    private bool trashWasRecycled;
    private TrashHandler TrashHandler;
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
        trashWasRecycled = false;
        TrashHandler = FindObjectOfType<TrashHandler>().GetComponent<TrashHandler>();
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
    }

    public void RecycleAtNearestMachine()
    {
        if (trashToRecycle.Count > 0)
        {
            RecycleAtNearestMachine(trashToRecycle[0]);
        }
        else
        {
            Debug.Log("No trash to recycle");
        }
    }

    private void RecycleAtNearestMachine(TrashScript trash)
    {
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
                    UpdateTrashDictionary(trash);
                    trashToRecycle.Remove(trash);
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

    public void AddTrashToRecycle(TrashScript trash)
    {
        trashToRecycle.Add(trash);
    }

    public void LoadData(GameData gameData)
    {
        trashToRecycle = gameData.FishedTrash;
    }

    public void SaveData(GameData gameData)
    {
        gameData.FishedTrash = trashToRecycle;
    }
}
