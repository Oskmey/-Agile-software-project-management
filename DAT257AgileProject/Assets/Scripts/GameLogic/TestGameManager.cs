using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    [Header("Managers")]
    private PlayerStatsManager playerStatsManager;
    private RecyclingManager recyclingManager;
    [Header("UI Elements")]
    [SerializeField] 
    private TextMeshProUGUI moneyGeneratedText;
    [SerializeField] 
    private TextMeshProUGUI recycledTrashCountText;
    [SerializeField]
    private TextMeshProUGUI recycleTrashLeftText;
    private int recycledTrashCount;

    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        recycledTrashCount = playerStatsManager.RecycledTrashDictionary.Count;
        UpdateTrashLeftText();
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
    }

    void UpdateTrashLeftText()
    {
        recycleTrashLeftText.text = "Recylcable trash Left: " + recyclingManager.TrashToRecycle.Count;
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
