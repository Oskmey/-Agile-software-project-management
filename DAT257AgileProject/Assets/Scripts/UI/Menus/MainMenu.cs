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
    [SerializeField]
    private Button settingsButton;

    private void Start()
    {
        // If there is no data shouldn't be able to continue.
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    public void OnNewGameClicked()
    {
        // Just to make sure the buttons aren't clicked more than once. 
        DisableMenuButtons();
        DataPersistenceManager.Instance.NewGame();
        LoadScene();
    }

    private static void LoadScene()
    {
        // Save game before loading scene. 
        DataPersistenceManager.Instance.SaveGame();
        // Loading the scene will load the game due to OnSceneLoaded in DataPersistenceManager.
        SceneManager.LoadSceneAsync("First World");
    }

    public void OnContinueClicked()
    {
        // Just to make sure the buttons aren't clicked more than once. 
        DisableMenuButtons();
        LoadScene();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
        settingsButton.interactable = false;
    }

    private void OnSettingsButtonClicked()
    {
        // Just to make sure the buttons aren't clicked more than once. 
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Settings Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
