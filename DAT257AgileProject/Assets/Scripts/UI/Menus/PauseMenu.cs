using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    private static bool GamePaused = false;

    [SerializeField] 
    private GameObject pauseMenuUI;
    private InputAction PauseAction;

    private PlayerInputActions playerInputActions;
    private bool SettingsOpen = false;

    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;
    //[SerializeField]
    //private Button settingsButton;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button quitButton;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        resumeButton.onClick.AddListener(OnPauseButtonClicked);
        //settingsButton.onClick.AddListener(Settings);
        menuButton.onClick.AddListener(OnMenuButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void Pause(InputAction.CallbackContext contex)
    {
        TogglePause();

    }

    private void OnPauseButtonClicked()
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

    public void OnMenuButtonClicked()
    {
        TogglePause();
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void OnQuitButtonClicked()
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
