using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMachine : MonoBehaviour
{
    private int moneyGenerated;
    private List<GameObject> trashRecycledList = new List<GameObject>();
    // the recycle machine should accept trash when pressing a button
    // it should generate money when trash is recycled
    // it should log the money generated
    // it should log the amount of trash recycled
    // it should log the trash that was recycled or not recycled

    public List<GameObject> TrashRecycledList
    {
        get
        {
            return trashRecycledList;
        }
        set
        {
            trashRecycledList = value;
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
        // should reject if not trash
        Debug.Log("Recycling trash");
        GenerateMoney(trash);
        TrashRecycledList.Add(trash);
        Destroy(trash);
    }

    void GenerateMoney(GameObject recycableTrash)
    {

        int money = 10;
        MoneyGenerated += money;
        Debug.Log("Money generated: " + money);
    }
}
