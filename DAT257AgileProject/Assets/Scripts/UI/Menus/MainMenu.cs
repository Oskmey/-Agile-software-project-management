using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IMenuWithSettings, IDataPersistence<GameData>
{
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button settingsButton;

    private SettingsMenu settingsMenu;
    private string sceneName;
    private void Start()
    {
        // If there is no data shouldn't be able to continue.
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueButton.interactable = false;
        }
        settingsMenu = FindObjectOfType<SettingsMenu>(true);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        AudioManager.Instance.PlayMusic(MusicName.MenuTheme);
    }

    public void OnNewGameClicked()
    {
        // Just to make sure the buttons aren't clicked more than once. 
        DisableMenuButtons();
        DataPersistenceManager.Instance.NewGame();
        LoadScene();
    }

    private void LoadScene()
    {
        // Save game before loading scene. 
        DataPersistenceManager.Instance.SaveGame();
        // Loading the scene will load the game due to OnSceneLoaded in DataPersistenceManager.
        SceneManager.LoadSceneAsync(sceneName);
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
        settingsMenu.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void LoadData(GameData data)
    {
        sceneName = data.CurrentLevel;
        
    }

    public void SaveData(GameData data)
    {
        // Unused in MainMenu
    }

}
