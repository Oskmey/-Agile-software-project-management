using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private List<GameObject> recycledTrashList;
    private int money;
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

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        recycledTrashList = new();
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
