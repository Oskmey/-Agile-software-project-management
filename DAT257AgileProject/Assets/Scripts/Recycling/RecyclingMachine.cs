using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    public void Recycle(GameObject trash)
    {
        Destroy(trash);
    }

    public bool IsPlayerInRange()
    {
        return GetComponentInChildren<RecyclingInteraction>().IsPlayerInRange;
    }
}
