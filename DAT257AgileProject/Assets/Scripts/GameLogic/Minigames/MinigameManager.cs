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
    private MinigameType currentMinigameType;
    private Minigame currentMinigame;
    private TextMeshProUGUI promptText;
 
    private bool minigameStarted = false;

    private void Awake()
    {
        GameObject promptTextObject = GameObject.FindGameObjectWithTag("TutorialText");
        if (promptTextObject != null)
        {
            promptText = promptTextObject.GetComponent<TextMeshProUGUI>();
        }
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
        //currentTrash = trashPrefab;
        minigameStarted = true;
        currentMinigameType = minigameType;
        InitMinigame(currentMinigameType);
    }

    void InitMinigame(MinigameType minigameType)
    {
        CreateMinigame(minigameType);
            }

    // Update is called once per frame
    void Update()
    {
        if(minigameStarted)
        {
            UpdateMinigamePromptText();
        }
    }

//Bör skicka med trashprefaben som ska spawnas aswell om man vill göra olika fishing tiles
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
        minigameStarted = false;
        promptText.text = "";
    }

    public void HandleMinigameLost()
    {
        //currentMinigame.DestroyMinigame();
        minigameStarted = false;
        //Debug.Log("Minigame lost! Implement your logic here...");
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