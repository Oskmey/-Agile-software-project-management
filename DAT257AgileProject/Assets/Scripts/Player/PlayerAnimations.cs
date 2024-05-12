using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerInteraction playerInteraction;

    private bool isFishing = false;
    private bool isPlaying = false;
    private Vector3 fishingSpotPos;
    private Vector3 directionToFishingSpot;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        if (playerInteraction.currentFishingSpot != null)
        {
            isFishing = true;
            isPlaying = playerInteraction.currentFishingSpot.GetComponent<FishingSpot>().IsPlayingMinigame;
            fishingSpotPos = playerInteraction.currentFishingSpot.transform.position;
        }
        else {
            isFishing = false;
            isPlaying = false;
        }

        if (fishingSpotPos != null)
        {
            directionToFishingSpot = fishingSpotPos - transform.position;
        }
      
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
        animator.SetBool("isFishing", isFishing);
        animator.SetBool("isPlaying", isPlaying);
        animator.SetFloat("Distance_x", directionToFishingSpot.x);
        animator.SetFloat("Distance_y", directionToFishingSpot.y);
    }
}

