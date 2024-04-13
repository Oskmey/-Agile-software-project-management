using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FishingFeature : MonoBehaviour
{
    [SerializeField] private ArrowBoxMinigame arrowBoxMinigame;
    [SerializeField] private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] private GameObject exclamationMarkPrefab;
    [SerializeField] private GameObject trashPrefab;    // TODO Make it possible to have many types of trash

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


    // Start is called before the first frame update
    void Start()
    {
        currentMinigame = MinigameType.ArrowBoxMinigame;
        trashHandler = GameObject.FindGameObjectWithTag("TrashHandler").GetComponent<TrashHandler>();
        playerInput = GetComponent<PlayerInput>();
        recycleAction = playerInput.actions["Recycle"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        fishAction = playerInput.actions["Fish"];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) 
        {
            GetComponent<SpriteRenderer>().sprite = fishingSprite1;
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

        if (fishAction.triggered && !isPlaying && canCatchTrash)
        {
            GetComponent<SpriteRenderer>().sprite = fishingSprite2;
            CreateMinigame(currentMinigame);
            isPlaying = true;                       // TODO Make it so that you can play again
            elapsedTime = 0f;
            canCatchTime = 0f;
        }

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
        // TODO: fix trashHandler being null when event invoked
        if (trashHandler == null)
        {
            Debug.LogError("TrashHandler not found.");
            return;
        }

        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y + 1);
        trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);
    }

    public void OnMinigameLostHandler()
    {
        Debug.Log("Minigame lost! Implement your logic here...");
    }
}
