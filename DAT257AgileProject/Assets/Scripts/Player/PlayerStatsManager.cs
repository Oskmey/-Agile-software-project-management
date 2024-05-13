using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence<GameData>
{
    public delegate void UpdateEvent();
    private SerializableDictionary<TrashType, int> recycledTrashDictionary;
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary
    {
        get => recycledTrashDictionary;
        private set 
        { 
            recycledTrashDictionary = value;
        } 
    }
    private SerializableDictionary<TrashType, int> trashCaughtDictionary;

    public SerializableDictionary<TrashType, int> TrashCaughtDictionary
    {
        get => trashCaughtDictionary;
        private set
        {
            trashCaughtDictionary = value;
        }
    }
    private int currentMoney;

    public int CurrentMoney
    {
        get => currentMoney;
        set
        {
            currentMoney = value;
        }
    }

    private int totalMoneyEarned;

    public int TotalMoneyEarned
    {
        get => totalMoneyEarned;
        set
        {
            totalMoneyEarned = value;
        }
    }
    private int totalMoneySpent;

    public int TotalMoneySpent
    {
        get => totalMoneySpent;
        set
        {
            totalMoneySpent = value;
        }
    }

    private SerializableDictionary<AccessorySO, int> purchasedAccessories;

    public SerializableDictionary<AccessorySO, int> PurchasedAccessories
    {
        get => purchasedAccessories;
        private set
        {
            purchasedAccessories = value;
        }
    }

    public void LoadData(GameData data)
    {
        RecycledTrashDictionary = data.RecycledTrashCount;
        TrashCaughtDictionary = data.TrashCaught;
        CurrentMoney = data.CurrentMoney;
        TotalMoneyEarned = data.TotalMoneyEarned;
        TotalMoneySpent = data.TotalMoneySpent;
        PurchasedAccessories = data.PurchasedAccessories;
    }

    public void SaveData(GameData data)
    {
        data.RecycledTrashCount = RecycledTrashDictionary;
        data.TrashCaught = TrashCaughtDictionary;
        data.CurrentMoney = CurrentMoney;
        data.TotalMoneyEarned = TotalMoneyEarned;
        data.TotalMoneySpent = TotalMoneySpent;
        data.PurchasedAccessories = PurchasedAccessories;
    }
}
