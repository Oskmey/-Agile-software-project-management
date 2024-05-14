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
    private Vector3 recyclingMachineSpotPos;
    private Vector3 directionToRecyclingMachineSpot;
    //private bool isRecycling = false;
    private RecyclingManager recyclingManager;
    private IReadOnlyList<RecyclingMachine> recyclingMachines;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        recyclingMachines = recyclingManager.GetRecyclingMachines();
    }


    // Update is called once per frame
    void Update()
    {
        //isRecycling = recyclingManager.IsRecycling;
        foreach (RecyclingMachine recyclingMachine in recyclingMachines)
        {
            if (recyclingMachine.IsPlayerInRange())
            {
               
                if (recyclingManager.IsRecycling)
                {
                    recyclingMachineSpotPos = recyclingMachine.transform.position;
                    directionToRecyclingMachineSpot = recyclingMachineSpotPos - transform.position;
                }
            }
        }

        if (playerInteraction.currentFishingSpot != null)
        {
            isFishing = true;
            isPlaying = playerInteraction.currentFishingSpot.GetComponent<FishingSpot>().isPlayingMinigame;
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
        animator.SetBool("isRecycling", recyclingManager.IsRecycling);
        animator.SetFloat("Distance_rm_x", directionToRecyclingMachineSpot.x);
        animator.SetFloat("Distance_rm_y", directionToRecyclingMachineSpot.y);
    }
}

