using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public FishingSpot currentFishingSpot;
    private bool isPlayerInRange;


    public bool IsPlayerInRange
    {
        get
        {
            return isPlayerInRange;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Fishing Spot"))
        {
            Debug.Log("Collision");
            currentFishingSpot = collision.gameObject.GetComponent<FishingSpot>();
            currentFishingSpot.HandleFishingPlaying();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fishing Spot"))
        {
            currentFishingSpot.ResetFishingLoop();
            currentFishingSpot = null;
        }
    }
}