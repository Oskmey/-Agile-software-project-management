using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour, IMenuWithSettings
{
    private static bool GamePaused = false;

    [SerializeField] 
    private GameObject pauseMenuUI;
    private InputAction PauseAction;

    private PlayerInputActions playerInputActions;

    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button quitButton;

    private SettingsMenu settingsMenu;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        settingsMenu = FindObjectOfType<SettingsMenu>(true);
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        resumeButton.onClick.AddListener(OnPauseButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
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
            if (!settingsMenu.IsActive())
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

    private void OnSettingsButtonClicked()
    {
        gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
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

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
