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
    private BlueBoxController blueBoxController;

    private GameObject arrow;
    private GameObject box;

    private PlayerInput playerControls;
    private InputAction catchTrash;

    // Start is called before the first frame update
    void Start()
    {
        promptText = string.Empty;
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        catchTrash = playerControls.actions["Catch"];
        onMinigameWon.AddListener(FindObjectOfType<MinigameManager>().HandleMinigameWon);
        onMinigameLost.AddListener(FindObjectOfType<MinigameManager>().GetComponent<MinigameManager>().HandleMinigameLost);
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (blueBoxController != null)
        {
            boxIsColliding = blueBoxController.BoxIsColliding();
        }

        if (catchTrash.triggered)
        {
            HandleCatch();
        }
    }

    public void HandleCatch()
    {
        if (boxIsColliding)
        {
            onMinigameWon.Invoke();
        }
        else
        {
            onMinigameLost.Invoke();
        }

        DestroyMinigame();
    }

    public override void StartMinigame()
    {
        arrow = Instantiate(arrowPrefab);
        box = Instantiate(boxPrefab);
        blueBoxController = box.GetComponent<BlueBoxController>();

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
    }
}
