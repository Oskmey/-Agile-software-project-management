using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FishingInteraction : MonoBehaviour
{
    private bool isPlayerInRange;

    public bool IsPlayerInRange
    {
        get
        {
            return isPlayerInRange;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //isPlayerInRange = true;
            //Debug.Log("Collision");
            //GetComponentInParent<FishingSpot>().HandleFishingPlaying();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //isPlayerInRange = false;
            //GetComponentInParent<FishingSpot>().ResetFishingLoop();
        }
    }
}
