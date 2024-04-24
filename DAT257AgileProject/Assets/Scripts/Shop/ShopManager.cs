using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RecyclingMachine;

public class ShopManager : MonoBehaviour
{
    private PlayerStatsManager playerStatsManager;
    private IReadOnlyList<Shop> shoppingSpots;
    
void Awake()
{
    shoppingSpots = GetShoppingSpots();
    playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
}


public void ShopAtNearestSpot()
{
    foreach (Shop shop in shoppingSpots)
    {
        // TODO: Make it so player can only recycle trash to nearest recycling machine
        // if (recyclingMachine.IsPlayerInRange(player.transform.position))
        
        if (shop.IsPlayerInRange())
        {
            Debug.Log("Player is in range of shopping");
            // NOTE: Trash is not recyclable by default, needs to be RecycableTrash
            
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