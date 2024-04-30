using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence
{
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary { get; private set; }

    public int Money { get; set; }

    public List<TrashType> FishedTrash { get; private set; }

    public void LoadData(GameData gameData)
    {
        RecycledTrashDictionary = gameData.RecycledTrashCount;
        Money = gameData.Money;
        FishedTrash = gameData.FishedTrash;
    }

    public void SaveData(GameData gameData)
    {
        gameData.RecycledTrashCount = RecycledTrashDictionary;
        gameData.Money = Money;
        gameData.FishedTrash = FishedTrash;
    }
}
