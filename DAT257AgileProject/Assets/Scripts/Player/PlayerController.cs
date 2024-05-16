using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour, IDataPersistence<GameData>
{
    public float speed { get; set; }

    private Rigidbody2D rb;
    private InputAction movementAction;
    private PlayerInput playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycleAction;
    private InputAction fishingAction;
    private InputAction shopAction;
    private ShopManager shoppingManager;
    private PlayerInteraction playerInteraction;

    private PlayerStatsManager playerStatsManager;

    private InputAction catchingAction; 

    private Minigame minigame;

    private bool canMove = true;

    private bool resultOfCatch;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["Movement"];
        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = FindObjectOfType<RecyclingManager>();
        fishingAction = GetComponent<PlayerInput>().actions["Fish"];
        shopAction = GetComponent<PlayerInput>().actions["Shop"];
        shoppingManager = FindObjectOfType<ShopManager>();
        catchingAction = GetComponent<PlayerInput>().actions["Catch"];
        MinigameManager miniGameManager = FindObjectOfType<MinigameManager>(); 

        if (miniGameManager != null)
        {
            minigame = miniGameManager.getCurrentMinigame();
        }

        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    void Start()
    {
        speed = 5f;    
    }

    private void OnEnable()
    {
        movementAction.performed += OnMovement;
        movementAction.canceled += OnMovementStopped;
        recycleAction.performed += Recycle;
        fishingAction.performed += Fishing;
        shopAction.performed += Shopping;
        catchingAction.performed += Catch;
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMovement;
        movementAction.canceled -= OnMovementStopped;
        recycleAction.performed -= Recycle;
        shopAction.performed -= Shopping;
        fishingAction.performed -= Fishing;
        catchingAction.performed -= Catch;
    }
    
    // Test method to recycle trash due to no inventory system
    private void Recycle(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            recyclingManager.RecycleAtNearestMachine();
        }
    }

    private void Fishing(InputAction.CallbackContext context)
    {   
        if (Time.timeScale > 0)
        {
            if (playerInteraction.currentFishingSpot != null)
            {
                canMove = false;
                rb.velocity = Vector2.zero;
                playerInteraction.currentFishingSpot.HandleMinigameStart();

            }
        }     
    }

    private void Shopping(InputAction.CallbackContext context)
    {   
        if (Time.timeScale > 0)
        {
            shoppingManager.ShopAtNearestSpot();
        }

    }

    private void OnMovement(InputAction.CallbackContext value)
    {
        if(canMove){
        rb.velocity = value.ReadValue<Vector2>() * speed;}
    }

    private void OnMovementStopped(InputAction.CallbackContext value)
    {
        rb.velocity = Vector2.zero;
    }

    //Måste lägga till minigames, cant be arsed
    private void Catch(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0)
        {
            if (playerInteraction.currentFishingSpot != null && playerInteraction.currentFishingSpot.GetIsPlaying() == true)
            {
                minigame = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MinigameManager>().getCurrentMinigame();
                resultOfCatch = minigame.HandleCatch();
                canMove = true;
                if (resultOfCatch)
                {
                    playerInteraction.currentFishingSpot.OnMinigameWonHandler();
                }
                else
                {
                    playerInteraction.currentFishingSpot.OnMinigameLostHandler();
                }
            }
    
        }
    }

    public void LoadData(GameData data)
    {
        transform.position = data.PlayerPosition;
    }

    public void SaveData(GameData data)
    {
        data.PlayerPosition = transform.position;
    }
}
