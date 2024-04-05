using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> recyclingMachines = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI moneyGeneratedText;
    [SerializeField] private TextMeshProUGUI recyclingCountText;
    // Start is called before the first frame update
    void Start()
    {
        recyclingMachines = GetRecyclingMachines();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Make it so recycling machines can recycle when near them
        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameObject trash = new GameObject();
                Debug.Log("Recycling trash");
                recyclingMachine.GetComponent<RecyclingMachine>().Recycle(trash);
            }
        }

        UpdateMoneyGeneratedText();
        UpdateRecyclingCountText();
    }

    void UpdateMoneyGeneratedText()
    {
        int moneyGenerated = 0;
        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            moneyGenerated += recyclingMachine.GetComponent<RecyclingMachine>().MoneyGenerated;
        }
        // error here because
        moneyGeneratedText.text = "Money Generated: " + moneyGenerated.ToString();
    }

    void UpdateRecyclingCountText()
    {
        int recycleCount = 0;
        List<GameObject> recyclingMachines = GetRecyclingMachines();
        foreach (GameObject recyclingMachine in recyclingMachines)
        {
            recycleCount += recyclingMachine.GetComponent<RecyclingMachine>().TrashRecycledList.Count;
        }
        recyclingCountText.text = "Recycling Count: " + recycleCount.ToString();
    }

    List<GameObject> GetRecyclingMachines()
    {
        List<GameObject> recyclingMachines = new List<GameObject>();
        GameObject[] recycleMachines = GameObject.FindGameObjectsWithTag("Recycle Machine");
        foreach (GameObject recycleMachine in recycleMachines)
        {
            Debug.Log("Recycling Machine found");
            recyclingMachines.Add(recycleMachine);
        }

        return recyclingMachines;
    }
}
