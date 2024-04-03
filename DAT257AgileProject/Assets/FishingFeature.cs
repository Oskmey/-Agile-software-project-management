using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingFeature : MonoBehaviour
{
    [SerializeField] FishingMinigame fishingMinigame;
    [SerializeField] Sprite fishingSprite1, fishingSprite2;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isPlaying)
        {
            GetComponent<SpriteRenderer>().sprite = fishingSprite2;
            fishingMinigame.startMinigame();
            isPlaying = true;
        }
    }
}
