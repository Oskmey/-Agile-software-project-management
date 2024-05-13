using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapInteraction : Ainteractable
{
    public override string text => "Press E to open map";

    public override void Interact()
    {
        Debug.Log("Map opened");
    }
}
