using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.InputSystem;
using static RecyclingMachine;

public class PlayerController : MonoBehaviour, IDataPersistence<GameData>
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private InputAction movementAction;
    private PlayerInput playerControls;
    private RecyclingManager recyclingManager;
    private InputAction recycleAction;
    private InputAction fishingAction;
    private InputAction intractionAction;
    private ShopManager shoppingManager;
    private PlayerInteraction playerInteraction;

    private PlayerStatsManager playerStatsManager;

    private InputAction catchingAction;

    private Minigame minigame;

    private bool canMove = true;

    private bool resultOfCatch;
    private Dictionary<Ainteractable, float> interactables;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["Movement"];
        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = GameObject.FindGameObjectWithTag("Recycling Manager").GetComponent<RecyclingManager>();
        fishingAction = GetComponent<PlayerInput>().actions["Fish"];
        intractionAction = GetComponent<PlayerInput>().actions["Shop"];
        shoppingManager = GameObject.FindGameObjectWithTag("Shop Manager").GetComponent<ShopManager>();
        catchingAction = GetComponent<PlayerInput>().actions["Catch"];
        minigame = GameObject.FindGameObjectWithTag("Minigame Manager").GetComponent<MinigameManager>().getCurrentMinigame();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    private void OnEnable()
    {
        movementAction.performed += OnMovement;
        movementAction.canceled += OnMovementStopped;
        recycleAction.performed += Recycle;
        fishingAction.performed += Fishing;
        intractionAction.performed += Interact;
        catchingAction.performed += Catch;
    }

    private void OnDisable()
    {
        movementAction.performed -= OnMovement;
        movementAction.canceled -= OnMovementStopped;
        recycleAction.performed -= Recycle;
        intractionAction.performed -= Interact;
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

    private void Interact(InputAction.CallbackContext context)
    {
        if (Time.timeScale > 0 && interactables != null)
        {
            float closestInteractable = interactables.Values.Max(); // Get the closest interactable value
            foreach (KeyValuePair<Ainteractable, float> interactable in interactables)
            {
                if (interactable.Value == closestInteractable)
                {
                    interactable.Key.Interact();
                    break;
                }
            }
        }
    }
    private void OnMovement(InputAction.CallbackContext value)
    {
        if (canMove)
        {
            rb.velocity = value.ReadValue<Vector2>() * speed;
        }
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

    public void AddInteractables(Ainteractable ainteractable)
    {
        float distanceToPlayer = ainteractable.DistanceToPlayer();
        if (distanceToPlayer >= 0)
        {
            interactables.Add(ainteractable, distanceToPlayer);
        }
        else
        {
            Debug.LogWarning("Distance to player is negative please check the code");
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
