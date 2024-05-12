using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence
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
        set
        {
            purchasedAccessories = value;
        }
    }

    public void LoadData(GameData gameData)
    {
        RecycledTrashDictionary = gameData.RecycledTrashCount;
        TrashCaughtDictionary = gameData.TrashCaught;
        CurrentMoney = gameData.CurrentMoney;
        TotalMoneyEarned = gameData.TotalMoneyEarned;
        TotalMoneySpent = gameData.TotalMoneySpent;
        PurchasedAccessories = gameData.PurchasedAccessories;
    }

    public void SaveData(GameData gameData)
    {
        gameData.RecycledTrashCount = RecycledTrashDictionary;
        gameData.TrashCaught = TrashCaughtDictionary;
        gameData.CurrentMoney = CurrentMoney;
        gameData.TotalMoneyEarned = TotalMoneyEarned;
        gameData.TotalMoneySpent = TotalMoneySpent;
        gameData.PurchasedAccessories = PurchasedAccessories;
    }
}
