using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayer : MonoBehaviour
{
    private int money;

    void Start()
    {
        //PlayerPrefs.SetInt("Money", 200);
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
        }
        else
        {
            Debug.Log("You are poor!");
        }
    }

    private void addItemToInventory(PurchasableItem.Type type)
    {
        Debug.Log(type + " is probably going to be added to your inventory");
    }
}
