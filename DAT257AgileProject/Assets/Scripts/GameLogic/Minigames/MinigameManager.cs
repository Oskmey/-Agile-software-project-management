using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MinigameManager : MonoBehaviour
{
    // Minigame prefabs
    [SerializeField] 
    private ArrowBoxMinigame arrowBoxMinigame;

    private TrashHandler trashHandler;

    private MinigameType currentMinigameType;
    private Minigame currentMinigame;
    private TextMeshProUGUI promptText;
 
    private bool minigameStarted = false;

    private TimingBoxController box;



    private void Awake()
    {
        trashHandler = GameObject.FindGameObjectWithTag("TrashHandler").GetComponent<TrashHandler>();
        promptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
    }

    public bool MinigameStarted
    {
        get
        {
            return minigameStarted;
        }
    }

    public Minigame getCurrentMinigame()
    {
        return currentMinigame;
    }

    public void StartMinigame(MinigameType minigameType)
    {
        minigameStarted = true;
        currentMinigameType = minigameType;
        InitMinigame(currentMinigameType);
    }

    void InitMinigame(MinigameType minigameType)
    {
        CreateMinigame(minigameType);
        //promptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(minigameStarted)
        {
            UpdateMinigamePromptText();
        }
    }

    private void CreateMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.ArrowBoxMinigame:
                currentMinigame = Instantiate(arrowBoxMinigame).GetComponent<Minigame>();
            
                // arrowBoxMinigame.transform.position = 
                break;
            case MinigameType.AnotherMinigame:
                //Instantiate(anotherPrefab);
                break;
            default:
                Debug.LogError($"Minigame type not supported: {type}");
                return;
        }
    }

    public void HandleMinigameWon()
    {
        currentMinigame.DestroyMinigame();
        minigameStarted = false;
        // TODO: fix objects being null when event invoked
        promptText.text = "";
        trashHandler = FindObjectOfType<TrashHandler>();
        
        if (trashHandler == null)
        {
            Debug.LogError("TrashHandler not found.");
            return;
        }

        Vector2 trashSpawnPosition = new(transform.position.x, transform.position.y + 1);
        trashHandler.CreateTrash(TrashType.TrashBag, trashSpawnPosition);
        PlayerPrefs.SetInt("RecycledTrashLeft", PlayerPrefs.GetInt("RecycledTrashLeft")+1);
        
    }

    public void HandleMinigameLost()
    {

        minigameStarted = false;
        Debug.Log("Minigame lost! Implement your logic here...");
        currentMinigame.DestroyMinigame();
    }

    private void UpdateMinigamePromptText()
    {
        switch (currentMinigameType)
        {
            case MinigameType.ArrowBoxMinigame:
                promptText.text = currentMinigame.PromptText;
                break;
            case MinigameType.AnotherMinigame:    
                break;
            default:
                Debug.LogError($"Minigame type not supported: {currentMinigameType}");
                return;
        }
    }
}
