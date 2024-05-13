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
    private bool settingsOpen = false;
    [SerializeField]
    private GameStatsUI gameStatsMenu;

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
        InitButtons();
    }

    private void InitButtons()
    {
        buttons = new List<Button>();

        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        resumeButton.onClick.AddListener(OnPauseButtonClicked);
        menuButton.onClick.AddListener(Menu);
        gameStatsButton.onClick.AddListener(OnGameStatsButtonClicked);
        quitButton.onClick.AddListener(Quit);

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
            if (settingsOpen == true || gameStatsMenu.gameObject.activeSelf)
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
        if (settingsOpen == false)
        {
            settingsOpen = true;
        }
        else
        {
            settingsOpen = false;
        }  
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
}
