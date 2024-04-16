using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private bool isFishing = false;
    private bool isPlaying = false;
    private Vector3 fishingSpotPos;
    private Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // TEMP Hardcoded for now
        GameObject fishingSpot = GameObject.FindGameObjectWithTag("Fishing Spot");
        if (fishingSpot != null)
        {
            fishingSpotPos = fishingSpot.transform.position;
        }
        else
        {
            Debug.Log("No fishing spot found");
        }
    }


    // Update is called once per frame
    void Update()
    {
        UglyTempFunction();
        CalculateDirectionToFishingSpot();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
        animator.SetBool("isFishing", isFishing);
        animator.SetBool("isPlaying", isPlaying);
        animator.SetFloat("Distance_x", direction.x);
        animator.SetFloat("Distance_y", direction.y);
    }

    private void CalculateDirectionToFishingSpot()
    {
        direction = fishingSpotPos - transform.position;
        direction = direction.normalized;
    }

    private void UglyTempFunction()
    {
        // TEMP!!!
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFishing = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isPlaying = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFishing = false;
            isPlaying = false;
        }

        //isFishing = get...
        //isPlaying = get...
    }
}

