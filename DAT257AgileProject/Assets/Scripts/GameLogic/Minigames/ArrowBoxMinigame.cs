using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ArrowBoxMinigame : MonoBehaviour, IMinigame
{
    [SerializeField] 
    private GameObject arrowPrefab;
    [SerializeField] 
    private GameObject boxPrefab;

    private bool boxIsColliding;
    private BlueBoxController blueBoxController;

    private GameObject arrow;
    private GameObject box;

    public UnityEvent onMinigameWon;
    public UnityEvent onMinigameLost;

    private PlayerInput playerControls;
    private InputAction catchTrash;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        onMinigameWon.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<MinigameManager>().OnMinigameWonHandler);
        onMinigameLost.AddListener(GameObject.FindGameObjectWithTag("Player").GetComponent<MinigameManager>().OnMinigameLostHandler);
        catchTrash = playerControls.actions["Catch"];
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (blueBoxController != null)
        {
            boxIsColliding = blueBoxController.BoxIsColliding();
        }

        if(catchTrash.triggered)
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

    public void StartMinigame()
    {
        arrow = Instantiate(arrowPrefab);
        box = Instantiate(boxPrefab);
        blueBoxController = box.GetComponent<BlueBoxController>();
    }

    public void DestroyMinigame() // Runs on both "Success" and "Very Bad!"
    {    
        if (box != null)
        {
            Destroy(box);
        }
        if (arrow != null)
        {
            Destroy(arrow);
        }
    }

}
