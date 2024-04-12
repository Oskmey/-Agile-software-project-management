using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycle;

    void Awake()
    {
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        playerControls = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
