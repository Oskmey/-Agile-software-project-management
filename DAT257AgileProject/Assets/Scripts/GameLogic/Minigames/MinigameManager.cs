using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] 
    private ArrowBoxMinigame arrowBoxMinigame;
    [SerializeField] 
    private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] 
    private GameObject exclamationMarkPrefab;
    [SerializeField]
    // TODO Make it possible to have many types of trash
    private GameObject trashPrefab;   

    private TrashHandler trashHandler;
    private PlayerInput playerInput;
    private InputAction recycleAction;
    private InputAction fishAction;
    private RecyclingManager recyclingManager;

    private bool isPlaying = false;
    private bool canCatchTrash = false;
    private float elapsedTime = 0f;
    private float delayTime = 4f;
    private float canCatchTime = 0f;
    private float canCatchDelay = 1.5f;

    private MinigameType currentMinigame;
    private TextMeshProUGUI tutorialText;
  
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
        currentMinigame = MinigameType.ArrowBoxMinigame;
        playerInput = GetComponent<PlayerInput>();
        recycleAction = playerInput.actions["Recycle"];
        fishAction = playerInput.actions["Fish"];
        trashHandler = GameObject.FindGameObjectWithTag("TrashHandler").GetComponent<TrashHandler>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleFishingPlaying();

        HandleFishingStart();

        HandleRecycle();
    }

    private void HandleFishingPlaying()
    {
        if (!isPlaying)
        {
            GetComponent<SpriteRenderer>().sprite = fishingSprite1;
            elapsedTime += Time.deltaTime;
            tutorialText.text = "Press F to Fish";

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

    private void HandleFishingStart()
    {
        if (fishAction.triggered && !isPlaying && canCatchTrash)
        {
            GetComponent<SpriteRenderer>().sprite = fishingSprite2;
            CreateMinigame(currentMinigame);
            isPlaying = true;
            tutorialText.text = "Press SPACE to catch";
        }
    }

    private void HandleRecycle()
    {
        if (recycleAction.triggered)
        {
            recyclingManager.RecycleAtNearestMachine();
        }
    }

    public void SpawnExclamationMark()
    {
        GameObject exclamationMark = Instantiate(exclamationMarkPrefab);
        exclamationMark.transform.localPosition += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
        Destroy(exclamationMark, 1.5f);
    }

    public void TrashCoughtEffect()     // Should spawn on success
    {
        Vector3 offset = new Vector3(3f, -2.5f, 0);     // The distance from player to float
        Vector3 spawnPos = transform.localPosition + offset;
        GameObject trash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);
        Destroy(trash, 1.5f);
    }

    private void CreateMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.ArrowBoxMinigame:
                Instantiate(arrowBoxMinigame);
                break;
            case MinigameType.AnotherMinigame:
                //Instantiate(anotherPrefab);
                break;
            default:
                Debug.LogError($"Minigame type not supported: {type}");
                return;
        }
    }

    public void OnMinigameWonHandler()
    {
        // TODO: fix objects being null when event invoked
        tutorialText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
        tutorialText.text = "";

        if (trashHandler == null)
        {
            Debug.LogError("TrashHandler not found.");
            return;
        }

        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y + 1);
        trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);

        Destroy(GameObject.FindGameObjectWithTag("Minigame"));
    }

    public void ResetMiniGame()
    {
        // Reset the game state
        isPlaying = false;
        canCatchTrash = false;
        elapsedTime = 0f;
        canCatchTime = 0f;
        tutorialText.text = "Press F to Fish";
    }

    public void OnMinigameLostHandler()
    {
        Debug.Log("Minigame lost! Implement your logic here...");
    }
}
