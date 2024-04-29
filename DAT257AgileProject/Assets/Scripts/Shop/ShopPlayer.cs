using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayer : MonoBehaviour, IDataPersistence
{
    private int money;

    public void TryToBuy(AccessorySO type)
    {
        Debug.Log($"You have: {money} money.");

        if (money >= type.cost)
        {
            Debug.Log("You had enough money!");
            money -= type.cost;
            AddItemToInventory(type);
            Debug.Log($"You know have: {money} money left.");
        }
        else
        {
            Debug.Log("You are poor!");
        }
    }

    private void AddItemToInventory(AccessorySO type)
    {
        // TODO Add to inventory
        Debug.Log(type.accessoryName + " is going to be added to your inventory");
    }

    public void LoadData(GameData gameData)
    {
        money = gameData.Money;
    }

    public void SaveData(GameData gameData)
    {
        gameData.Money = money;
    }
}
