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
    private GameStatsUI gameStatsMenu;
    private SettingsMenu settingsMenu;

    [Header("Buttons")]
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button gameStatsButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button quitButton;

    private List<Button> buttons;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        settingsMenu = FindObjectOfType<SettingsMenu>(true);
        InitButtons();
    }

    private void InitButtons()
    {
        buttons = new List<Button>();

        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        resumeButton.onClick.AddListener(OnPauseButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        menuButton.onClick.AddListener(OnMenuButtonClicked);
        gameStatsButton.onClick.AddListener(OnGameStatsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        buttons.Add(pauseButton);
        buttons.Add(resumeButton);
        buttons.Add(menuButton);
        buttons.Add(gameStatsButton);
        buttons.Add(quitButton);
    }

    public void HidePauseButtons()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowPauseButtons()
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void Pause(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (GamePaused)
        {
            if (settingsMenu.IsActive() || gameStatsMenu.gameObject.activeSelf)
            {
                return;
            }
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

    private void OnPauseButtonClicked()
    {
        TogglePause();
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

    public void OnGameStatsButtonClicked()
    {
        HidePauseButtons();
        gameStatsMenu.gameObject.SetActive(true);
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
