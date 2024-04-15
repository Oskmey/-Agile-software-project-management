using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycle;
    private InputAction recycleAction;

    void Awake()
    {
        playerControls = GetComponent<PlayerInput>();

        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
    }

    private void OnEnable()
    {
        recycleAction.performed += Recycle;
    }

    private void OnDisable()
    {
        recycleAction.performed -= Recycle;
    }

    // Test method to recycle trash due to no inventory system
    private void Recycle(InputAction.CallbackContext context)
    {
        if (!FishingLoop.IsFishing)
        {
            recyclingManager.RecycleAtNearestMachine();
        }
        /*
        else
        {
            Debug.Log("Can't recycle if not in range of machine or fishing");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
