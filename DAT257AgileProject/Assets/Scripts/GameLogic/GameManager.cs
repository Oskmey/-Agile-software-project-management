using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("RecycledTrashCount", 0);
        PlayerPrefs.SetInt("RecycledTrashLeft", 0);

        fishingLoop = FindObjectOfType<FishingLoop>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();

        recyclingManager = FindObjectOfType<RecyclingManager>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        recycleAction = playerInput.actions["Recycle"];
    }

    // TEMO savesystem
    private void Save()
    {
        playerStatsManager.Save();
        recyclingManager.Save();
    }

    // Fishing is currently switching to another scene
    public void StartFishing()
    {
        FishingLoop.IsFishing = true;
        SceneManager.LoadSceneAsync("Fishing");
        Save();
    }

    public void StopFishing()
    {
        FishingLoop.IsFishing = false;
        SceneManager.LoadSceneAsync("World");
        Save();
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
        if (recycleAction.triggered && !FishingLoop.IsFishing)
        {
            recyclingManager.RecycleAtNearestMachine();
        }
    }

    void UpdateTrashLeftText()
    {
        recycleTrashLeftText.text = "Trash left to recycle: " + recyclingManager.TrashToRecycle.Count;
    }

    void UpdateMoneyGenerated()
    {
        moneyGeneratedText.text = "Money Generated: " + playerStatsManager.Money.ToString();
    }

    void UpdateRecycledTrashCountText()
    {
        recycledTrashCountText.text = "Trash recycled: " + recycledTrashCount.ToString();
    }
}
