using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RecyclingMachine;

public class FishingManager : MonoBehaviour
{
    private PlayerStatsManager playerStatsManager;
    private IReadOnlyList<FishingSpot> fishingSpots;
    
void Awake()
{
    
    fishingSpots = GetFishingSpots();
    playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
}


public void FishAtNearestSpot()
{
    foreach (FishingSpot fishingSpot in fishingSpots)
    {
        // TODO: Make it so player can only recycle trash to nearest recycling machine
        // if (recyclingMachine.IsPlayerInRange(player.transform.position))
        
        if (fishingSpot.IsPlayerInRange())
        {
            Debug.Log("Player is in range of fishing");
            // NOTE: Trash is not recyclable by default, needs to be RecycableTrash
            
        }
        
    }
}


public IReadOnlyList<FishingSpot> GetFishingSpots()
    {
        List<FishingSpot> fishSpots = new();
        GameObject[] fishingSpots = GameObject.FindGameObjectsWithTag("Fishing Spot");

        foreach (GameObject fishingSpot in fishingSpots)
        {
            fishingSpot.GetComponent<FishingSpot>();
            fishSpots.Add(fishingSpot.GetComponent<FishingSpot>());
        }

        return fishSpots;
    }

}