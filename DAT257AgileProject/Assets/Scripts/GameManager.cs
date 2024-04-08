using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    private TextMeshProUGUI recycleSucessText;
    [SerializeField]
    private TextMeshProUGUI recycleTrashLeftText;
    private int recycledTrashCount;

    // Start is called before the first frame update
    void Start()
    {
        playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();

        // TEMP: adding trash to recycle
        GenerateTrash(2);
    }

    void GenerateTrash(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<RecyclingMachine.RecycableTrash>();
            recyclingManager.TrashToRecycle.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        recycledTrashCount = playerStatsManager.RecycledTrashList.Count;
        UpdateTrashLeftText();
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();

        if (recyclingManager.TrashToRecycle.Count == 0)
        {
            recycleSucessText.text = "No trash to recycle";
        }
        else
        {
            UpdateRecycleSucessText();
        }
    }

    void UpdateTrashLeftText()
    {
        recycleTrashLeftText.text = "Recycable trash Left: " + recyclingManager.TrashToRecycle.Count;
    }

    void UpdateRecycleSucessText()
    {
        if (recyclingManager.TrashWasRecycled)
        {
            recycleSucessText.text = "Recycling Sucess";
        }
        else if(!recyclingManager.TrashWasRecycled)
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
