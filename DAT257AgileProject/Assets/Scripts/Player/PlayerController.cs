using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    private InputAction intractionAction;
    private PlayerInteraction playerInteraction;

    private PlayerStatsManager playerStatsManager;

    private InputAction catchingAction;

    private Minigame minigame;

    private bool canMove = true;

    private bool resultOfCatch;
    private HashSet<Ainteractable> interactables = new HashSet<Ainteractable>();



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementAction = GetComponent<PlayerInput>().actions["Movement"];
        recycleAction = GetComponent<PlayerInput>().actions["Recycle"];
        recyclingManager = FindObjectOfType<RecyclingManager>();
        fishingAction = GetComponent<PlayerInput>().actions["Fish"];
        intractionAction = GetComponent<PlayerInput>().actions["Interact"];
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
            Ainteractable closestInteractable = null;
            float smallestDistance = float.MaxValue;
            foreach (Ainteractable interactable in interactables)
            {
                float distance = interactable.DistanceToPlayer();
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestInteractable = interactable;
                }
            }
            if (closestInteractable != null)
            {
                closestInteractable.Interact();
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

    public void AddInteractable(Ainteractable ainteractable)
    {
            interactables.Add(ainteractable);
    }

    public void RemoveInteractable(Ainteractable ainteractable)
    {
        interactables.Remove(ainteractable);
    }

    public void LoadData(GameData data)
    {
        transform.position = data.GetPlayerPosition(SceneManager.GetActiveScene().name);
    }

    public void SaveData(GameData data)
    {
        data.CurrentLevel = SceneManager.GetActiveScene().name;
        data.SetPlayerPosition(SceneManager.GetActiveScene().name, transform.position);
    }
}
