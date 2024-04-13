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

    public bool IsTrashRecyclable(GameObject trash)
    {
        return trash.GetComponent<RecyclableTrash>() != null;
    }

    // This is a test class to simulate trash
    public class RecyclableTrash : Trash
    {
 
    }

    public class Trash : MonoBehaviour
    {
        public int trashValue = 10;
    }
}
