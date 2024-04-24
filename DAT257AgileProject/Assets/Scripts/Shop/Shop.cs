
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{


public bool IsPlayerInRange()
{
    return GetComponentInChildren<ShopInteraction>().IsPlayerInRange;
}


}