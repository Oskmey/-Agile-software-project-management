using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingFeature : MonoBehaviour
{
    [SerializeField] private ArrowBoxMinigame arrowBoxMinigame;
    [SerializeField] private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] private GameObject exclamationMarkPrefab;
    [SerializeField] private GameObject trashPrefab;    // TODO Make it possible to have many types of trash

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying) 
        { 
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

        if (Input.GetKeyDown(KeyCode.F) && !isPlaying && canCatchTrash)
        {
            GetComponent<SpriteRenderer>().sprite = fishingSprite2;
            CreateMinigame(currentMinigame);
            isPlaying = true;                       // TODO Make it so that you can play again
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
}
