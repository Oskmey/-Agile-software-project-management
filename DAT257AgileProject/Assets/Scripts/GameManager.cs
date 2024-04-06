using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private RecyclingManager recyclingMachineManager;
    private List<GameObject> recyclingMachines = new();
    [SerializeField] private TextMeshProUGUI moneyGeneratedText;
    [SerializeField] private TextMeshProUGUI recycledTrashCountText;

    // Start is called before the first frame update
    void Start()
    {
        recyclingMachineManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        recyclingMachines = recyclingMachineManager.GetRecyclingMachines();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
    }

    void UpdateMoneyGenerated()
    {
        moneyGeneratedText.text = "Money Generated: " + recyclingMachineManager.GetTotalGeneratedMoney().ToString();
    }

    void UpdateRecycledTrashCountText()
    {
        recycledTrashCountText.text = "Recycling Count: " + recyclingMachineManager.GetRecycledTrashCount().ToString();
    }
}
