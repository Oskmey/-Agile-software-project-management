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
    private bool isRecycling;
    public bool IsRecycling => isRecycling;
    private IReadOnlyList<RecyclingMachine> recyclingMachines;
    private PlayerStatsManager playerStatsManager;
    private bool trashWasRecycled;
    private float moneyMultiplier = 1f;
    public float MoneyMultiplier
    {
        get { return moneyMultiplier; }
        set { moneyMultiplier = value; }
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
        isRecycling = false;
        recyclingMachines = GetRecyclingMachines();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
    }

    public void RecycleAtNearestMachine()
    {
        foreach (RecyclingMachine recyclingMachine in recyclingMachines)
        {
            if (recyclingMachine.IsPlayerInRange())
            {
                List<TrashItemSO> trashToRecycle = playerInventory.GetAndRemoveRecyclableTrashItems();
                StartCoroutine(RecyclingInteraction());
                foreach (TrashItemSO trash in trashToRecycle)
                {
                    AudioManager.Instance.PlaySound(SoundName.RecycleNoise);
                    int recyclingMoney = (int)(trash.TrashData.MoneyValue * moneyMultiplier);
                    playerStatsManager.CurrentMoney += recyclingMoney;
                    playerStatsManager.TotalMoneyEarned += recyclingMoney;
                    UpdateRecycledTrashDictionary(trash.TrashType);
                }
            }
        }
    }

    private void UpdateRecycledTrashDictionary(TrashType trashType)
    {
        if (playerStatsManager.RecycledTrashDictionary.ContainsKey(trashType))
        {
            playerStatsManager.RecycledTrashDictionary[trashType]++;
        }
        else
        {
            playerStatsManager.RecycledTrashDictionary.Add(trashType, 1);
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

    IEnumerator RecyclingInteraction()
    {
        isRecycling = true;
        yield return new WaitForSeconds((float)0.5);
        isRecycling = false;
    }
}
