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

    void Start(){
        trashToRecycle = LoadTrash();
    }

    void Awake()
    {
        
        trashWasRecycled = false;
        TrashHandler = FindObjectOfType<TrashHandler>().GetComponent<TrashHandler>();
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
    }

    // TEMP: basic temporary saving system between scenes
    private List<TrashScript> LoadTrash()
    {
        List<TrashScript> trashToRecycle = new();
        int trashToRecycleLeft = PlayerPrefs.GetInt("RecycledTrashLeft");

        if (trashToRecycleLeft > 0)
        {
            for (int i = 0; i < trashToRecycleLeft; i++)
            {
                GameObject tempTrash = TrashFactory.CreateTrash(TrashType.TrashBag);
                trashToRecycle.Add(tempTrash.GetComponent<TrashScript>());
                Destroy(tempTrash);
            }
        }

        return trashToRecycle;
    }

    // TEMP SAVE
    public void Save()
    {
        PlayerPrefs.SetInt("RecycledTrashLeft", trashToRecycle.Count);
    }

    public void RecycleAtNearestMachine()
    {
        trashToRecycle = LoadTrash();
        if (trashToRecycle.Count > 0)
        {
            
            RecycleAtNearestMachine(trashToRecycle[0]);
        }
        else{
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
                    Debug.Log(playerStatsManager.Money);
                    playerStatsManager.Money += trash.MoneyValue;
                    playerStatsManager.RecycledTrashList.Add(trash);
                    trashToRecycle.Remove(trash);
                    trashWasRecycled = true;
                    TrashHandler.DestroyTrash();
                    Debug.Log("Sold!");
                    Debug.Log(playerStatsManager.Money);
                    PlayerPrefs.SetInt("RecycledTrashLeft", PlayerPrefs.GetInt("RecycledTrashLeft")-1);
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
