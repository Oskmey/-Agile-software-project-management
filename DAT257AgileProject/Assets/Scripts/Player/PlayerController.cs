using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private InputAction movementAction;
    private PlayerInput playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycleAction;

    private InputAction fishingAction;
    private FishingManager fishingManager;

    private InputAction shopAction;
    private ShopManager shoppingManager;

    private PlayerInteraction playerInteraction;

    private InputAction catchingAction; 

    private Minigame minigame;

    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["Movement"];

        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();

        fishingAction = GetComponent<PlayerInput>().actions["Fish"];
        fishingManager = GameObject.FindGameObjectWithTag("Fishing Manager").GetComponent<FishingManager>();

        shopAction = GetComponent<PlayerInput>().actions["Shop"];
        shoppingManager = GameObject.FindGameObjectWithTag("Shop Manager").GetComponent<ShopManager>();

        catchingAction = GetComponent<PlayerInput>().actions["Catch"];
        minigame = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MinigameManager>().getCurrentMinigame();

        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    private void OnEnable()
    {
        movementAction.performed += OnMovment;
        movementAction.canceled += OnMovmentStopped;
        recycleAction.performed += Recycle;
        fishingAction.performed += Fishing;
        shopAction.performed += Shopping;
        catchingAction.performed += Catch;
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMovment;
        movementAction.canceled -= OnMovmentStopped;
        recycleAction.performed -= Recycle;
        shopAction.performed -= Shopping;
        catchingAction.performed -= Catch;
    }
    
    // Test method to recycle trash due to no inventory system
    private void Recycle(InputAction.CallbackContext context)
    {
        Debug.Log("Recycling trash if in range");
        recyclingManager.RecycleAtNearestMachine();
    }

    private void Fishing(InputAction.CallbackContext context)
    {
        Debug.Log("Fishing if in range");
        if (playerInteraction.currentFishingSpot != null){
            canMove=false;
        rb.velocity = Vector2.zero;
        playerInteraction.currentFishingSpot.HandleMinigameStart();
        
        }
        
    }

        private void Shopping(InputAction.CallbackContext context)
    {
        Debug.Log("Shopping if in range");
        shoppingManager.ShopAtNearestSpot();
    }

    private void OnMovment(InputAction.CallbackContext value)
    {
        if(canMove){
        rb.velocity = value.ReadValue<Vector2>() * speed;}
    }

    private void OnMovmentStopped(InputAction.CallbackContext value)
    {
        rb.velocity = Vector2.zero;
    }

    //Måste lägga till minigames, cant be arsed
    private void Catch(InputAction.CallbackContext context)
    {if (playerInteraction.currentFishingSpot != null){
        
        Debug.Log("Catching");
        minigame = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MinigameManager>().getCurrentMinigame();
        minigame.HandleCatch();}
        canMove=true;
    }

}
