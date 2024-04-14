using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class ArrowBoxMinigame : Minigame
{
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

    private bool isFishing = false;
    private bool canCatchTrash = false;
    private float elapsedTime = 0f;
    private float delayTime = 4f;
    private float canCatchTime = 0f;
    private float canCatchDelay = 1.5f;

    private SpriteRenderer playerSpriteRenderer;

    [SerializeField] 
    private GameObject arrowPrefab;
    [SerializeField] 
    private GameObject boxPrefab;

    private bool boxIsColliding;
    private BlueBoxController blueBoxController;

    private GameObject arrow;
    private GameObject box;

    private PlayerInput playerControls;
    private InputAction catchTrash;

    // Start is called before the first frame update
    void Start()
    {
        promptText = "Press F to Fish";

        recyclingManager = FindObjectOfType<RecyclingManager>();
        playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        onMinigameWon.AddListener(FindObjectOfType<MinigameManager>().HandleMinigameWon);
        onMinigameLost.AddListener(FindObjectOfType<MinigameManager>().HandleMinigameLost);

        catchTrash = playerControls.actions["Catch"];
        recycleAction = playerControls.actions["Recycle"];
        fishAction = playerControls.actions["Fish"];
    }

    // Update is called once per frame
    void Update()
    {
        if (blueBoxController != null)
        {
            boxIsColliding = blueBoxController.BoxIsColliding();
        }

        HandleCatch();
        HandleFishingPlaying();
        HandleFishingStart();
        HandleRecycle();
    }

    private void HandleFishingPlaying()
    {
        if (!isFishing)
        {
            playerSpriteRenderer.sprite = fishingSprite1;
            elapsedTime += Time.deltaTime;
            promptText = "Press F to Fish";

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

    public void ResetMiniGame()
    {
        // Reset the game state
        isFishing = false;
        canCatchTrash = false;

        elapsedTime = 0f;
        canCatchTime = 0f;
        promptText = "Press F to Fish";
    }

    private void HandleFishingStart()
    {
        if (fishAction.triggered && !isFishing && canCatchTrash)
        {
            playerSpriteRenderer.sprite = fishingSprite2;
            isFishing = true;
            SpawnBoxTiming();
            promptText  = "Press SPACE to catch";
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

    public void HandleCatch()
    {
        if (catchTrash.triggered)
        {
            if (boxIsColliding)
            {
                onMinigameWon.Invoke();
            }
            else
            {
                onMinigameLost.Invoke();
            }

            DestroyBoxTiming();
        }
    }

    public override void StartMinigame()
    {

    }

    public override void DestroyMinigame()
    {

    }

    private void SpawnBoxTiming()
    {
        arrow = Instantiate(arrowPrefab);
        box = Instantiate(boxPrefab);
        blueBoxController = box.GetComponent<BlueBoxController>();
    }

    private void DestroyBoxTiming()
    {
        if (box != null)
        {
            Destroy(box);
        }
        if (arrow != null)
        {
            Destroy(arrow);
        }
    }

    public override void HandleMinigameWon()
    {
        // TODO: fix objects being null when event invoked
        promptText = "";
        trashHandler = FindObjectOfType<TrashHandler>();
        if (trashHandler == null)
        {
            Debug.LogError("TrashHandler not found.");
            return;
        }

        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y + 1);
        trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);
    }

    public override void HandleMinigameLost()
    {
        Debug.Log("Minigame lost! Implement your logic here...");
    }

    public override void ResetMinigame()
    {
        isFishing = false;
        canCatchTrash = false;

        elapsedTime = 0f;
        canCatchTime = 0f;
        promptText = "Press F to Fish";
    }
}
