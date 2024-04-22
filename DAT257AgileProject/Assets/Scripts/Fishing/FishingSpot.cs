
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
    private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] 
    private FishingSpotRarities trashRarity;
    [SerializeField] 
    private GameObject exclamationMarkPrefab;


    private FishingSpotRarities fs;
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

    private static Dictionary<TrashRarity, double> rarityPercentages = new Dictionary<TrashRarity, double>();
    
    private static System.Random rnd = new System.Random();
//Need to add percentages I guess
void Start()
    {
        GetPercentages();
        listOfRarities = GetRarities(trashRarity);
        currentRarity=rnd.Next(listOfRarities.Count);
        fishSpot = GetComponent<FishingSpot>();
        promptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
        minigameManager = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MinigameManager>();
        trashHandler = GameObject.FindGameObjectWithTag("TrashHandler").GetComponent<TrashHandler>();
    }

private void GetPercentages(){
    //Använder common som alltid i tickad
    //Går såklart att ändra rarity percentages
    //rarityPercentages.Add(TrashRarity.Common, 0.30);
    rarityPercentages.Add(TrashRarity.Uncommon, 0.4);
    rarityPercentages.Add(TrashRarity.Rare, 0.3);
    rarityPercentages.Add(TrashRarity.Epic, 0.2);
    rarityPercentages.Add(TrashRarity.Legendary, 0.10);
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

    public bool GetIsPlaying(){
        return isPlayingMinigame;
    }

    public void ResetFishingLoop()
    {
        // Reset the game state
        currentRarity = rnd.Next(listOfRarities.Count);
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
        isPlayingMinigame=false;
        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y);
        TrashScript currentTrash = trashHandler.CreateRandomTrash(listOfRarities[currentRarity], trashSpawnPosition);
    }

    public void OnMinigameLostHandler()
    {
        isPlayingMinigame=false;
        Debug.Log("Minigame lost! Implement your logic here...");
    }



//Add them to list as many times as their percentage represent, then randomize the list when u fish, idk how to do this otherwise, might not be most efficient but it works :=).

    public List<TrashRarity> GetRarities(FishingSpotRarities fishingSpotRarities){
        List<TrashRarity> tempList = new List<TrashRarity>();
        
        foreach(FishingSpotRarities rarity in Enum.GetValues(typeof(FishingSpotRarities))){
            if(fishingSpotRarities.HasFlag(rarity)){
                
                switch(rarity){
                    
                    // case FishingSpotRarities.Common:
                    // for (int i = 0; i < (rarityPercentages[TrashRarity.Common]*tempIntCount*10); i++) {
                    // tempList.Add(TrashRarity.Common);}
                    // break;
                    

                    case FishingSpotRarities.Uncommon: 
                    for (int i = 0; i < (rarityPercentages[TrashRarity.Uncommon]*10); i++) {
                    tempList.Add(TrashRarity.Uncommon);}
                    break;

                    case FishingSpotRarities.Rare: 
                    for (int i = 0; i < (rarityPercentages[TrashRarity.Rare]*10); i++) {
                    tempList.Add(TrashRarity.Rare);}
                    break;

                    case FishingSpotRarities.Epic: 
                    for (int i = 0; i < (rarityPercentages[TrashRarity.Epic]*10); i++) {
                    tempList.Add(TrashRarity.Epic);}
                    break;

                    case FishingSpotRarities.Legendary: 
                    for (int i = 0; i < (rarityPercentages[TrashRarity.Legendary]*10); i++) {
                    tempList.Add(TrashRarity.Legendary);
                    }
                    break;

                }
            }
        }
        while(tempList.Count < 10){
            
            tempList.Add(TrashRarity.Common);
        }
        return tempList;
        

    }

}