using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    private TextMeshProUGUI promptText;

    public void Recycle(GameObject trash)
    {
        Destroy(trash);
    }

    public bool IsPlayerInRange()
    {
        return GetComponentInChildren<RecyclingInteraction>().IsPlayerInRange;
    }
}
