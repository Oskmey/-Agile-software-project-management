
using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Common.GameUI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FishingSpot : MonoBehaviour
{
    [SerializeField] 
    private FishingSpotRarities fishingSpotRarities;

    [SerializeField] 
    private GameObject exclamationMarkPrefab;
    private static bool isFishing = true;
    public bool IsPlayingMinigame { get; private set; }
    private bool canCatchTrash = false;
    private float elapsedTime = 0f;
    private float delayTime = 2f;
    private float canCatchTime = 0f;
    private float canCatchDelay = 1.5f;
    private MinigameManager minigameManager;
    private TextMeshProUGUI promptText;

    private TrashHandler trashHandler;

    private List<float> listOfRarities;

    private TrashRarity currentRarity;

    //Need to add percentages I guess
    void Start()
    {
        listOfRarities = new List<float>(fishingSpotRarities.ToList());
        currentRarity = GetCurrentRarity(listOfRarities);
        promptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
        minigameManager = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MinigameManager>();
        trashHandler = GameObject.FindGameObjectWithTag("TrashHandler").GetComponent<TrashHandler>();
    }

    //Triggered when walking close, borde vara collider grejs
    public void HandleFishingPlaying()
    {
        if (isFishing)
        {    
            promptText.text = "Press F to Fish";
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= delayTime)
            {
                //Debug.Log("Player is in range of fishing");
            
                if (elapsedTime >= delayTime)
                {
                    if (!canCatchTrash)
                    {
                        SpawnExclamationMark();
                    }
                    canCatchTrash = true;
                    canCatchTime += Time.deltaTime;

                    if (canCatchTime >= canCatchDelay)
                    {
                        canCatchTrash = false;
                        elapsedTime = 0f;
                        canCatchTime = 0f;
                    }
                }
            }
        }
    }
    public void HandleMinigameStart()
    {
        if (!IsPlayingMinigame && canCatchTrash)
        {   
            minigameManager.StartMinigame(MinigameType.ArrowBoxMinigame);
            
            IsPlayingMinigame = true;
            isFishing = false;

            elapsedTime = 0f;
            canCatchTime = 0f;
            promptText.text = "Press SPACE to catch";
        }
    }

    public void SpawnExclamationMark()
    {
        GameObject exclamationMark = Instantiate(exclamationMarkPrefab);
        exclamationMark.transform.position = transform.position;
        Destroy(exclamationMark, 1.5f);
    }

    public bool GetIsPlaying()
    {
        return IsPlayingMinigame;
    }

    public void ResetFishingLoop()
    {
        // Reset the game state
        currentRarity = GetCurrentRarity(listOfRarities);
        isFishing = true;
        IsPlayingMinigame = false;
        canCatchTrash = false;

        elapsedTime = 0f;
        canCatchTime = 0f;

        promptText.text = "";
    }

    //Should spawn the trashPrefab for each spot
    public void OnMinigameWonHandler()
    {
        // TODO: fix trashHandler being null when event invoked
        IsPlayingMinigame=false;
        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y);
        TrashScript currentTrash = trashHandler.CreateRandomTrash(currentRarity, trashSpawnPosition);
    }

    public void OnMinigameLostHandler()
    {
        IsPlayingMinigame=false;
        Debug.Log("Minigame lost! Implement your logic here...");
    }
    public TrashRarity GetCurrentRarity(List<float> listOfRarityPercentages)
    {

        //You yourself have to make sure it adds up to 100% or 1.0
        float randomNumber = UnityEngine.Random.value;

        if (randomNumber <= listOfRarityPercentages[0])
        {
            return TrashRarity.Common;
        }

        if (randomNumber <= listOfRarityPercentages[1] && randomNumber > listOfRarityPercentages[0])
        {
            return TrashRarity.Uncommon;
        }

        if (randomNumber <= listOfRarityPercentages[2] && randomNumber > listOfRarityPercentages[1])
        {
            return TrashRarity.Rare;
        }

        if (randomNumber <= listOfRarityPercentages[3] && randomNumber > listOfRarityPercentages[2])
        {
            return TrashRarity.Epic;
        }

        if (randomNumber <= listOfRarityPercentages[4] && randomNumber > listOfRarityPercentages[3])
        {
            return TrashRarity.Legendary;
        }

        //Will never get here, in the best of worlds where it adds up to 100% :-).
        return TrashRarity.Common;
    }
}