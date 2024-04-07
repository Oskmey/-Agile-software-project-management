using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishingFeature : MonoBehaviour
{
    [SerializeField] private FishingMinigame fishingMinigame;
    [SerializeField] private Sprite fishingSprite1, fishingSprite2;
    [SerializeField] private GameObject exclamationMarkPrefab;

    private bool isPlaying = false;
    private bool canCatchTrash = false;

    private float elapsedTime = 0f;
    private float delayTime = 4f;
    private float canCatchTime = 0f;
    private float canCatchDelay = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        
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
            fishingMinigame.startMinigame();        // TODO Make it possible to switch minigame
            isPlaying = true;                       // TODO Make it so that you can play again
        }
    }

    public void SpawnExclamationMark()
    {
            GameObject exclamationMark = Instantiate(exclamationMarkPrefab);
            exclamationMark.transform.localPosition += new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0f);
            Destroy(exclamationMark, 1.5f);
    }
}
