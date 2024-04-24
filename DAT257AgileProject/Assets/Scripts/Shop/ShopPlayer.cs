using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayer : MonoBehaviour
{
    private int money;

    void Start()
    {

    }

    public void TryToBuy(PurchasableItem.Type type)
    {
        money = PlayerPrefs.GetInt("Money");
        Debug.Log("You have: " + money + " money");

        if (money >= PurchasableItem.GetCost(type))
        {
            Debug.Log("You had enough money!");
            addItemToInventory(type);
            PlayerPrefs.SetInt("Money", money - PurchasableItem.GetCost(type));
            Debug.Log("You know have: " + PlayerPrefs.GetInt("Money") + " money left");
        }
        else
        {
            Debug.Log("You are poor!");
        }
    }

    private void addItemToInventory(PurchasableItem.Type type)
    {
        // TODO Add to inventory
        Debug.Log(type + " is going to be added to your inventory");
    }
}
