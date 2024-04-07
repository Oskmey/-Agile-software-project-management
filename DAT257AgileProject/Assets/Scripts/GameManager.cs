using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private RecyclingManager recyclingManager;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI moneyGeneratedText;
    [SerializeField] private TextMeshProUGUI recycledTrashCountText;
    [SerializeField] private TextMeshProUGUI recycleSucessText;
    private int recycledTrashCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        recycledTrashCount = playerStatsManager.RecycledTrashList.Count;
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
        UpdateRecycleSucessText();
    }

    void UpdateRecycleSucessText()
    {
        if (recyclingManager.TrashWasRecycled)
        {
            recycleSucessText.text = "Recycling Sucess";
        }
        else
        {
            recycleSucessText.text = "Recycling Failed";
        }

    }

    void UpdateMoneyGenerated()
    {
        moneyGeneratedText.text = "Money Generated: " + playerStatsManager.Money.ToString();
    }

    void UpdateRecycledTrashCountText()
    {
        recycledTrashCountText.text = "Recycled Trash Count: " + recycledTrashCount.ToString();
    }
}
