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
    private TextMeshProUGUI minigamePromptText;

    // Start is called before the first frame update
    void Start()
    {
        // startminigame
        currentMinigameType = MinigameType.ArrowBoxMinigame;
        CreateMinigame(currentMinigameType);
        minigamePromptText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMinigamePromptText();
    }

    private void CreateMinigame(MinigameType type)
    {
        switch (type)
        {
            case MinigameType.ArrowBoxMinigame:
                currentMinigame = Instantiate(arrowBoxMinigame).GetComponent<Minigame>();
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
        currentMinigame = GameObject.FindGameObjectWithTag("Minigame").GetComponent<Minigame>();
        currentMinigame.HandleMinigameWon();
    }

    public void HandleMinigameLost()
    {
        currentMinigame = GameObject.FindGameObjectWithTag("Minigame").GetComponent<Minigame>();
        currentMinigame.HandleMinigameLost();
    }

    public void ResetMiniGame()
    {
        //Destroy(GameObject.FindGameObjectWithTag("Minigame"));

        currentMinigame = GameObject.FindGameObjectWithTag("Minigame").GetComponent<Minigame>();
        currentMinigame.ResetMinigame();
    }

    private void UpdateMinigamePromptText()
    {
        switch (currentMinigameType)
        {
            case MinigameType.ArrowBoxMinigame:
                minigamePromptText.text = currentMinigame.PromptText;
                break;
            case MinigameType.AnotherMinigame:
                
                break;
            default:
                Debug.LogError($"Minigame type not supported: {currentMinigameType}");
                return;
        }
    }
}
