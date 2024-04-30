using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence
{
    private int money;
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary { get; set; }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public List<TrashType> FishedTrash { get; private set; }

    public void LoadData(GameData gameData)
    {
        RecycledTrashDictionary = gameData.RecycledTrashCount;
        money = gameData.Money;
        FishedTrash = gameData.FishedTrash;
    }

    public void SaveData(GameData gameData)
    {
        gameData.RecycledTrashCount = RecycledTrashDictionary;
        gameData.Money = money;
        gameData.FishedTrash = FishedTrash;
    }
}
