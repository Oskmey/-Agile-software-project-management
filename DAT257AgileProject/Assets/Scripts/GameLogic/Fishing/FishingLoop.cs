using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FishingLoop : MonoBehaviour
{
    [SerializeField] 
    private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] 
    private GameObject exclamationMarkPrefab;
    [SerializeField] 
    private GameObject trashPrefab;    // TODO Make it possible to have many types of trash

    private PlayerInput playerInput;
    private InputAction fishAction;

    private static bool isFishing = false;
    private bool isPlayingMinigame = false;
    private bool canCatchTrash = false;
    private float elapsedTime = 0f;
    private float delayTime = 4f;
    private float canCatchTime = 0f;
    private float canCatchDelay = 1.5f;

    private MinigameManager minigameManager;
    private TextMeshProUGUI promptText;

    private SpriteRenderer playerSpriteRenderer;

    public static bool IsFishing
    {
        get { return isFishing; }
        set { isFishing = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        promptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
        minigameManager = FindObjectOfType<MinigameManager>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        fishAction = playerInput.actions["Fish"];
    }

    // Update is called once per frame
    void Update()
    {
         HandleFishingPlaying();
         HandleMinigameStart();     
    }

    private void HandleFishingPlaying()
    {
        if (isFishing)
        {
            promptText.text = "Press F to Fish";
            playerSpriteRenderer.sprite = fishingSprite1;
            elapsedTime += Time.deltaTime;

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

    private void HandleMinigameStart()
    {
        if (fishAction.triggered && !isPlayingMinigame && canCatchTrash)
        {
            playerSpriteRenderer.sprite = fishingSprite2;
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
        exclamationMark.transform.localPosition += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
        Destroy(exclamationMark, 1.5f);
    }

    public void ResetFishingLoop()
    {
        // Reset the game state
        isFishing = true;
        isPlayingMinigame = false;
        canCatchTrash = false;

        elapsedTime = 0f;
        canCatchTime = 0f;

        promptText.text = "Press F to Fish";

        Destroy(GameObject.FindGameObjectWithTag("Minigame"));   
    }

    public void TrashCoughtEffect()     // Should spawn on success
    {
        Vector3 offset = new Vector3(3f, -2.5f, 0);     // The distance from player to float
        Vector3 spawnPos = transform.localPosition + offset;
        GameObject trash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);
        Destroy(trash, 1.5f);
    }
}