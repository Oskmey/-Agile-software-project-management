using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    private static bool GamePaused = false;

    [SerializeField] private GameObject pauseMenuUI;
    private InputAction PauseAction;

    private PlayerInputActions playerInputActions;
    private bool SettingsOpen = false;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

    }

    private void Pause(InputAction.CallbackContext contex)
    {
        TogglePause();

    }

    public void PauseButton()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (GamePaused)
        {
            if (SettingsOpen == false)
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;

        }
        else
        {   
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GamePaused = true;
  
        }
    }

    public void Menu()
    {
        TogglePause();
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (SettingsOpen == false)
        {
            SettingsOpen = true;
        }
        else
        {
            SettingsOpen = false;
        }
        
    }

    private void OnEnable()
    {
        PauseAction = playerInputActions.UI.Pause;
        PauseAction.Enable();
        PauseAction.performed += Pause;
    }

    private void OnDisable()
    {
        PauseAction.Disable();
    }
}
