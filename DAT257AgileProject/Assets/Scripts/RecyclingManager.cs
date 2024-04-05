using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingManager : MonoBehaviour
{
    private List<GameObject> recyclingMachines = new();

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

    void Update()
    {
        // TODO: Make it so recycling machines can recycle when near them
        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // TEMP: Creating trash to recycle
                GameObject trash = new();
                trash.AddComponent<RecyclingMachine.RecycableTrash>();

                recyclingMachine.GetComponent<RecyclingMachine>().Recycle(trash);
            }
        }
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
