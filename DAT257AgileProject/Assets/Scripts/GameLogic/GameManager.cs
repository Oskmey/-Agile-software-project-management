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
        ResetSave();
        // fishingLoop = FindObjectOfType<FishingLoop>();
        playerStatsManager = FindObjectOfType<PlayerStatsManager>();
        recyclingManager = FindObjectOfType<RecyclingManager>();
    }

    // TEMP savesystem
    private void ResetSave()
    {
        PlayerPrefs.SetInt("RecycledTrashLeft", 0);
        PlayerPrefs.SetInt("RecycledTrashCount", 0);
        PlayerPrefs.SetInt("Money", 0);
    }

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
        UpdateMoneyGenerated();
    }

    void UpdateMoneyGenerated()
    {
        moneyGeneratedText.text = "Money Generated: " + playerStatsManager.Money.ToString();
    }
}
