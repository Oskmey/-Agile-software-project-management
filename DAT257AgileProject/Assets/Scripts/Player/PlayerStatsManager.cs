using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private List<TrashScript> recycledTrashList;
    private int money;
    public List<TrashScript> RecycledTrashList
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
        recycledTrashList = LoadTrashRecycled();

        money = PlayerPrefs.GetInt("Money");

        if (money == 0)
        {
            PlayerPrefs.SetInt("Money", 0);
        }
    }

    // TEMP: basic temporary saving system between scenes

    private List<TrashScript> LoadTrashRecycled()
    {
        List<TrashScript>  recycledTrashList = new();
        int trashToRecycle = PlayerPrefs.GetInt("RecycledTrashCount");

        if (trashToRecycle > 0)
        {
            for (int i = 0; i < trashToRecycle; i++)
            {
                GameObject tempTrash = TrashFactory.CreateTrash(TrashType.TrashBag);
                recycledTrashList.Add(tempTrash.GetComponent<TrashScript>());
                Destroy(tempTrash);
            }
        }
        return recycledTrashList;
    }

    // TEMP SAVE
    public void Save()
    {
        PlayerPrefs.SetInt("RecycledTrashCount", recycledTrashList.Count);
        PlayerPrefs.SetInt("Money", money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
