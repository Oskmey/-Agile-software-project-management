using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    private PlayerStatsManager playerStatsManager;
    private RecyclingManager recyclingManager;
    private FishingLoop fishingLoop;

    [Header("UI Elements")]
    [SerializeField] 
    private TextMeshProUGUI moneyGeneratedText;
    [SerializeField] 
    private TextMeshProUGUI recycledTrashCountText;
    [SerializeField]
    private TextMeshProUGUI recycleTrashLeftText;
    private int recycledTrashCount;

    private PlayerInput playerInput;
    private InputAction recycleAction;


    // Start is called before the first frame update
    void Start()
    {
        fishingLoop = FindObjectOfType<FishingLoop>();
        playerStatsManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsManager>();
        recyclingManager = FindObjectOfType<RecyclingManager>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        recycleAction = playerInput.actions["Recycle"];

        fishingLoop.StartFishing();
    }

    // Update is called once per frame
    void Update()
    {
        recycledTrashCount = playerStatsManager.RecycledTrashList.Count;
        UpdateTrashLeftText();
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
        HandleRecycle();
    }

    private void HandleRecycle()
    {
        if (recycleAction.triggered)
        {
            recyclingManager.RecycleAtNearestMachine();
        }
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
