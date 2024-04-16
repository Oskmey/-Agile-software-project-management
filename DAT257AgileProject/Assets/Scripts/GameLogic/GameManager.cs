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

    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("RecycledTrashCount", 0);
        PlayerPrefs.SetInt("RecycledTrashLeft", 0);

        fishingLoop = FindObjectOfType<FishingLoop>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        recyclingManager = FindObjectOfType<RecyclingManager>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    // TEMO savesystem
    private void Save()
    {
        if (playerStatsManager == null)
        {
            Debug.LogError("playerStatsManager is null");
        }
        else
        {
            playerStatsManager.Save();

        }

        if (recyclingManager == null)
        {
            Debug.LogError("recyclingManager is null");
        }
        else 
        {
            recyclingManager.Save();
        }
    }

    // Fishing is currently switching to another scene
    public void StartFishing()
    {
        Save();
        FishingLoop.IsFishing = true;
        SceneManager.LoadSceneAsync("Fishing");
    }

    public void StopFishing()
    {
        Save();
        FishingLoop.IsFishing = false;
        SceneManager.LoadSceneAsync("World");
    }
         
    // Update is called once per frame
    void Update()
    {
        recycledTrashCount = playerStatsManager.RecycledTrashList.Count;
        UpdateTrashLeftText();
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
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