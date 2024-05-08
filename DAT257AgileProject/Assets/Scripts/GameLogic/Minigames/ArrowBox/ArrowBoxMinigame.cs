using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class ArrowBoxMinigame : Minigame
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject boxPrefab;

    private bool boxIsColliding;
    private TimingBoxController timingBoxController;

    private GameObject arrow;
    private GameObject box;

    private PlayerInput playerControls;
    private InputAction catchTrash;

    private Transform playerPos ;

    private Vector3 offsetArrow;

    private Vector3 offsetBox;
    // Start is called before the first frame update
    void Start()
    {
        promptText = string.Empty;
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Subscribe to the events
        OnMinigameWon += FindObjectOfType<MinigameManager>().HandleMinigameWon;
        OnMinigameLost += FindObjectOfType<MinigameManager>().HandleMinigameLost;
        OnMinigameLost += () => FindObjectOfType<GameplayHudHandler>().UpdateWarningPopup("You lost the minigame");

        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (timingBoxController != null)
        {
            boxIsColliding = timingBoxController.BoxIsColliding();
        }
    }

    public override bool HandleCatch()
    {
        if (boxIsColliding)
        {
            MinigameWon();
            DestroyMinigame();
            return true;
        }
        else
        {
            MinigameLost();
            DestroyMinigame();
            return false;
        }

        
    }

    void OnDestroy()
    {
        OnMinigameWon -= FindObjectOfType<MinigameManager>().HandleMinigameWon;
        OnMinigameLost -= FindObjectOfType<MinigameManager>().HandleMinigameLost;
        OnMinigameLost -= () => FindObjectOfType<GameplayHudHandler>().UpdateWarningPopup("You lost the minigame");
    }

    public override void StartMinigame()
    {
        arrow = Instantiate(arrowPrefab);
        box = Instantiate(boxPrefab);
        offsetArrow = new Vector3(0,2.15f,0);
        offsetBox = new Vector3(-2f,1.7f,0);
        arrow.transform.position = playerPos.position + offsetArrow;
        box.transform.position = playerPos.position + offsetBox;
        timingBoxController = box.GetComponent<TimingBoxController>();

        promptText = "Press SPACE to catch";
    }

    public override void DestroyMinigame() // Runs on both "Success" and "Very Bad!"
    {
        if (box != null)
        {
            Destroy(box);
        }
        if (arrow != null)
        {
            Destroy(arrow);
        }

        promptText = "";
        Destroy(gameObject);
    }


}
