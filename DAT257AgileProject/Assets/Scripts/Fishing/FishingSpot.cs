
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FishingSpot : MonoBehaviour
{

    [SerializeField] 
    private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] 
    private FishingSpotRarities trashRarity;
    [SerializeField] 
    private GameObject exclamationMarkPrefab;

    private static bool isFishing = true;
    private bool isPlayingMinigame = false;
    private bool canCatchTrash = false;
    private float elapsedTime = 0f;
    private float delayTime = 2f;
    private float canCatchTime = 0f;
    private float canCatchDelay = 1.5f;
    private MinigameManager minigameManager;
    private TextMeshProUGUI promptText;

    private SpriteRenderer playerSpriteRenderer;

    private FishingSpot fishSpot;

    private TrashHandler trashHandler;

    private List<TrashRarity> listOfRarities;

    private int currentRarity;
    
//Need to add percentages I guess
void Start()
    {
        currentRarity=0;
        listOfRarities = GetRarities(trashRarity);
        fishSpot = GetComponent<FishingSpot>();
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
        //playerSpriteRenderer.sprite = fishingSprite1;
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
        if (!isPlayingMinigame && canCatchTrash)
        {
            //playerSpriteRenderer.sprite = fishingSprite2;
            
            minigameManager.StartMinigame(MinigameType.ArrowBoxMinigame);
            
            isPlayingMinigame = true;
            isFishing = false;

            elapsedTime = 0f;
            canCatchTime = 0f;
            promptText.text = "Press SPACE to catch";
        }
    }

    public void SpawnExclamationMark()
    {
        GameObject exclamationMark = Instantiate(exclamationMarkPrefab);
        exclamationMark.transform.position = fishSpot.transform.position;
        Destroy(exclamationMark, 1.5f);
    }

    public void ResetFishingLoop()
    {
        // Reset the game state
        currentRarity = (currentRarity + 1) % listOfRarities.Count;
        isFishing = true;
        isPlayingMinigame = false;
        canCatchTrash = false;
        //playerSpriteRenderer.sprite = fishingSprite2;

        elapsedTime = 0f;
        canCatchTime = 0f;

        promptText.text = "";
    }



//Should spawn the trashPrefab for each spot
    public void OnMinigameWonHandler()
    {
        // TODO: fix trashHandler being null when event invoked

        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y);
        TrashScript currentTrash = trashHandler.CreateRandomTrash(listOfRarities[currentRarity], trashSpawnPosition);
    }

    public void OnMinigameLostHandler()
    {
        Debug.Log("Minigame lost! Implement your logic here...");
    }




    public List<TrashRarity> GetRarities(FishingSpotRarities fishingSpotRarities){
        List<TrashRarity> tempList = new List<TrashRarity>();
        foreach(FishingSpotRarities rarity in Enum.GetValues(typeof(FishingSpotRarities))){
            if(fishingSpotRarities.HasFlag(rarity)){
                
                switch(rarity){
                    
                    case FishingSpotRarities.Common: 
                    tempList.Add(TrashRarity.Common);
                    break;

                    case FishingSpotRarities.Uncommon: 
                    tempList.Add(TrashRarity.Uncommon);
                    break;

                    case FishingSpotRarities.Rare: 
                    tempList.Add(TrashRarity.Rare);
                    break;

                    case FishingSpotRarities.Epic: 
                    tempList.Add(TrashRarity.Epic);
                    break;

                    case FishingSpotRarities.Legendary: 
                    tempList.Add(TrashRarity.Legendary);
                    break;

                }
                
            
            }
            
            
        }
        return tempList;
        

    }

}