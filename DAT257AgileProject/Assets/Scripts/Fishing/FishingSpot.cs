
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour
{


public bool IsPlayerInRange()
{
    return GetComponentInChildren<FishingInteraction>().IsPlayerInRange;
}



}