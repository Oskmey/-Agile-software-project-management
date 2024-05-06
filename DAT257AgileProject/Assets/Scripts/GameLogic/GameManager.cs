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
    // private FishingLoop fishingLoop;

    [Header("UI Elements")]
    [SerializeField] 
    private TextMeshProUGUI moneyGeneratedText;

    // Start is called before the first frame update
    void Awake()
    {
        // fishingLoop = FindObjectOfType<FishingLoop>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        recyclingManager = FindObjectOfType<RecyclingManager>();
    }

    private void Save()
    {
        if (playerStatsManager == null)
        {
            Debug.LogError("playerStatsManager is null");
        }
        else
        {
            //playerStatsManager.Save();

        }

        if (recyclingManager == null)
        {
            Debug.LogError("recyclingManager is null");
        }
        else 
        {
            //recyclingManager.Save();
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
        UpdateMoneyGenerated();
        UpdateRecycledTrashCountText();
    }

    void UpdateTrashLeftText()
    {
        //recycleTrashLeftText.text = "Trash left to recycle: " + recyclingManager.TrashToRecycle.Count;
    }

    void UpdateMoneyGenerated()
    {
        moneyGeneratedText.text = "Money Generated: " + playerStatsManager.Money.ToString();
    }

    void UpdateRecycledTrashCountText()
    {
        //recycledTrashCountText.text = "Trash recycled: " + playerStatsManager.RecycledTrashDictionary.Count.ToString();
        moneyGeneratedText.text = "Money: " + playerStatsManager.Money.ToString();
    }
}
