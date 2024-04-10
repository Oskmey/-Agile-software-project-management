using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    // TODO: implement interaction range with player
    private float interactionRange;
    
    void Start()
    {
        interactionRange = 5f;
    }

    public float InteractionRange
    {
        get
        {
            return interactionRange;
        }
    }

    public void Recycle(GameObject trash)
    {
        Destroy(trash);
    }

    public bool IsPlayerInRange(Vector2 playerPosition)
    {
        return Vector2.Distance(playerPosition, transform.position) <= interactionRange;
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
