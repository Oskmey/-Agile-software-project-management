using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RecyclingMachine;

public class RecyclingManager : MonoBehaviour
{
    private List<GameObject> recyclingMachines = new();
    [SerializeField]
    private PlayerStatsManager playerStatsManager;

    public List<GameObject> RecyclingMachines
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

    void Start()
    {
        recyclingMachines = GetRecyclingMachines();
    }

    public void RecycleAtNearestMachine(GameObject trash)
    {
        // TODO: add range to recycling machines
        // TODO: Make it so recycling machines can recycle when near them
        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            // TEMP: Creating trash to recycle
            trash.AddComponent<RecycableTrash>();

            recyclingMachine.GetComponent<RecyclingMachine>().Recycle(trash);
        }

        playerStatsManager.TrashRecycledList.Add(trash);
        playerStatsManager.Money += trash.GetComponent<RecycableTrash>().trashValue;

        Debug.Log("we are recycling");
    }

    public List<GameObject> GetRecyclingMachines()
    {
        List<GameObject> recyclingMachines = new List<GameObject>();
        GameObject[] recycleMachines = GameObject.FindGameObjectsWithTag("Recycle Machine");
        foreach (GameObject recycleMachine in recycleMachines)
        {
            recyclingMachines.Add(recycleMachine);
        }

        return recyclingMachines;
    }

    public int GetTotalGeneratedMoney()
    {
        int moneyGenerated = 0;

        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            moneyGenerated += recyclingMachine.GetComponent<RecyclingMachine>().MoneyGenerated;
        }

        return moneyGenerated;
    }

    public int GetRecycledTrashCount()
    {
        int recycleCount = 0;

        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            recycleCount += recyclingMachine.GetComponent<RecyclingMachine>().TrashRecycledList.Count;
        }

        return recycleCount;
    }
}
