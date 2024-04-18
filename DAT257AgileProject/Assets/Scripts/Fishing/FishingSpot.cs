
using System.Collections;
using System.Collections.Generic;
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



void Start()
    {
        
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
        isFishing = true;
        isPlayingMinigame = false;
        canCatchTrash = false;
        //playerSpriteRenderer.sprite = fishingSprite2;

        elapsedTime = 0f;
        canCatchTime = 0f;

        promptText.text = "";
    }


//Kallas inte right now
    public void TrashCoughtEffect()     // Should spawn on success
    {
        Vector3 offset = new Vector3(3f, -2.5f, 0);     // The distance from player to float
        Vector3 spawnPos = transform.localPosition + offset;
        //GameObject trash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);
        //Destroy(trash, 1.5f);
    }

//Should spawn the trashPrefab for each spot
    public void OnMinigameWonHandler()
    {
        // TODO: fix trashHandler being null when event invoked
        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y);
        trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);
    }

    public void OnMinigameLostHandler()
    {
        Debug.Log("Minigame lost! Implement your logic here...");
    }

}