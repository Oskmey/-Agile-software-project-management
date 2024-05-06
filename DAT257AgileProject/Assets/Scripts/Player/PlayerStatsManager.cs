using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence
{
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary { get; private set; }

    public int Money { get; set; }

    public void LoadData(GameData gameData)
    {
        RecycledTrashDictionary = gameData.RecycledTrashCount;
        Money = gameData.Money;
    }

    public void SaveData(GameData gameData)
    {
        gameData.RecycledTrashCount = RecycledTrashDictionary;
        gameData.Money = Money;
    }
}
