using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ShopInteraction : Ainteractable
{
    public override string text => "Press E to Shop";

    public override void Interact()
    {
        Debug.Log("Shop opened");
    }
    
}
