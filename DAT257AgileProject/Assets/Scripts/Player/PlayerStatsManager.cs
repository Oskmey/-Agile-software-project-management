using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence<GameData>
{
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary { get; private set; }

    public int Money { get; set; }

    public void LoadData(GameData data)
    {
        RecycledTrashDictionary = data.RecycledTrashCount;
        Money = data.Money;
    }

    public void SaveData(GameData data)
    {
        data.RecycledTrashCount = RecycledTrashDictionary;
        data.Money = Money;
    }
}
