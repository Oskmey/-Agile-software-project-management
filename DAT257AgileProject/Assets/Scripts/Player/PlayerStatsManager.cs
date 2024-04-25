using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour, IDataPersistence
{
    private SerializableDictionary<TrashType, int> recycledTrashDictionary;
    private int money;
    public SerializableDictionary<TrashType, int> RecycledTrashDictionary
    {
        get
        {
            return recycledTrashDictionary;
        }
        set
        {
            recycledTrashDictionary = value;
        }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public void LoadData(GameData gameData)
    {
        recycledTrashDictionary = gameData.RecycledTrashCount;
        money = gameData.Money;
    }

    public void SaveData(GameData gameData)
    {
        gameData.RecycledTrashCount = recycledTrashDictionary;
        gameData.Money = money;
    }
}
