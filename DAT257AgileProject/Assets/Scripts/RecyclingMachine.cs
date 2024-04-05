using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    private int moneyGenerated;
    private List<GameObject> recycledTrashList = new();
    // the recycle machine should accept trash when pressing a button
    // it should generate money when trash is recycled
    // it should log the money generated
    // it should log the amount of trash recycled
    // it should log the trash that was recycled or not recycled

    public List<GameObject> TrashRecycledList
    {
        get
        {
            return recycledTrashList;
        }
        set
        {
            recycledTrashList = value;
        }
    }

    public int MoneyGenerated
    {
        get
        {
            return moneyGenerated;
        }
        set
        {
            moneyGenerated = value;
        }
    }

    public void Recycle(GameObject trash)
    {
        RecycableTrash recycableTrash = trash.GetComponent<RecycableTrash>();
        // TODO: should reject if not trash
        if (recycableTrash == null)
        {
            Debug.Log("Not recyclable trash");
            return;
        }

        Debug.Log("Recycling trash");

        GenerateMoney(recycableTrash.trashValue);
        TrashRecycledList.Add(trash);
        Destroy(trash);
    }

    void GenerateMoney(int recycableTrashValue)
    {
        MoneyGenerated += recycableTrashValue;
        Debug.Log("Money generated: " + recycableTrashValue);
    }

    // This is a test class to simulate trash
    public class RecycableTrash : MonoBehaviour
    {
        public int trashValue = 10;
    }
}
