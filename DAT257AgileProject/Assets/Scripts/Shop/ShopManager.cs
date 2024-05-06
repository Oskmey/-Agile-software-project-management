using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static RecyclingMachine;

public class ShopManager : MonoBehaviour
{
    private IReadOnlyList<Shop> shoppingSpots;
    
    void Awake()
    {
        shoppingSpots = GetShoppingSpots();
    }


    public void ShopAtNearestSpot()
    {
        foreach (Shop shop in shoppingSpots)
        {
            // TODO: Make it so player can only recycle trash to nearest recycling machine
            // if (recyclingMachine.IsPlayerInRange(player.transform.position))
        
            if (shop.IsPlayerInRange())
            {
                DataPersistenceManager.Instance.SaveGame();
                SceneManager.LoadScene("Shop");
            }
        
        }
    }


    public IReadOnlyList<Shop> GetShoppingSpots()
    {
        List<Shop> shopSpots = new();
        GameObject[] shoppingSpots = GameObject.FindGameObjectsWithTag("Shop");

        foreach (GameObject shop in shoppingSpots)
        {
            shop.GetComponent<Shop>();
            shopSpots.Add(shop.GetComponent<Shop>());
        }

        return shopSpots;
    }

}