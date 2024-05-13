using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence<GameData>
{
    public delegate void UpdateEvent();
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary { get; private set; }
    public SerializableDictionary<TrashType, int> TrashCaughtDictionary { get; private set; }
    public int CurrentMoney { get; set; }
    public int TotalMoneyEarned { get; set; }
    public int TotalMoneySpent { get; set; }
    public SerializableDictionary<AccessorySO, int> PurchasedAccessories { get; private set; }
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
