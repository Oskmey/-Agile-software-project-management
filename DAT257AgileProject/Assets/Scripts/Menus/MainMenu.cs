using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button continueButton;

    private void Start()
    {
        // If there is no data shouldn't be able to continue.
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
    }

    // Not needed anymore.
    public void PlayGame() 
    {
        LoadScene();
    }

    public void OnNewGameClicked()
    {
        // Just to make sure the buttons aren't clicked more than once. 
        DisableMenuButtons();
        DataPersistenceManager.Instance.NewGame();
        // Save game before loading scene. 
        DataPersistenceManager.Instance.SaveGame();
        LoadScene();
    }

    private static void LoadScene()
    {
        // Loading the scene will load the game due to OnSceneLoaded in DataPersistenceManager.
        SceneManager.LoadSceneAsync("Oskar's World");
    }

    public void OnContinueClicked()
    {
        // Just to make sure the buttons aren't clicked more than once. 
        DisableMenuButtons();
        // Save game before loading scene. 
        DataPersistenceManager.Instance.SaveGame();
        LoadScene();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
