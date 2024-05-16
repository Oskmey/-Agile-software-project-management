using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IMenuWithSettings, IMenuWithHowToPlay
{
    [Header("Buttons")]
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button howToPlayButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button wikiButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button quitButton;

    private SettingsMenu settingsMenu;
    private HowToPlayMenu howToPlayMenu;

    private void Start()
    {
        // If there is no data shouldn't be able to continue.
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
        settingsMenu = FindObjectOfType<SettingsMenu>(true);
        howToPlayMenu = FindObjectOfType<HowToPlayMenu>(true);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        AudioManager.Instance.PlayMusic(MusicName.MenuTheme);
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
        howToPlayButton.interactable = false;
        settingsButton.interactable = false;
        wikiButton.interactable = false;
        creditsButton.interactable = false;
        quitButton.interactable = false;
    }

    private void OnSettingsButtonClicked()
    {
        settingsMenu.gameObject.SetActive(true);
    }

    private void OnHowToPlayButtonClicked()
    {
        howToPlayMenu.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
