using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider masterVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider soundVolumeSlider;

    [SerializeField]
    private Button backButton;

    private IMenuWithSettings otherMenuGameObject;


    private PlayerInputActions playerInputActions;
    private InputAction BackAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        InitializeUIComponents();

        otherMenuGameObject = FindMenuWithSettings();
        if (otherMenuGameObject == null)
        {
            Debug.LogError("otherMenuGameObject was null");
        }
    }

    private void OnEnable()
    {
        BackAction = playerInputActions.UI.Pause;
        BackAction.Enable();
        BackAction.performed += Back;
    }

    private void Back(InputAction.CallbackContext context)
    {
        OnBackButtonClicked();
    }

    private void OnDisable()
    {
        BackAction.Disable();
    }

    private void InitializeUIComponents()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        InitializeMasterSlider();
        InitializeMusicSlider();
        InitializeSoundSlider();
    }

    private void InitializeMasterSlider()
    {
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeSliderValueChanged);
        masterVolumeSlider.value = AudioManager.Instance.MasterVolume;
    }

    private void InitializeMusicSlider()
    {
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChanged);
        musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
    }

    private void InitializeSoundSlider()
    {
        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderValueChanged);
        soundVolumeSlider.value = AudioManager.Instance.SoundVolume;
    }

    private IMenuWithSettings FindMenuWithSettings()
    {
        IMenuWithSettings menuWithSettings = FindObjectOfType<MainMenu>(true);
        if (menuWithSettings != null)
        {
            return menuWithSettings;
        }
        menuWithSettings = FindObjectOfType<PauseMenu>(true);
        if (menuWithSettings != null)
        {
            return menuWithSettings;
        }

        Debug.LogError("Could not find menu with settings");
        return null;
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    private void OnBackButtonClicked()
    {
        otherMenuGameObject.GetGameObject().SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnMasterVolumeSliderValueChanged(float newVolume)
    {
        AudioManager.Instance.SetMasterVolume(newVolume);
    }

    private void OnMusicVolumeSliderValueChanged(float newVolume)
    {
        AudioManager.Instance.SetMusicVolume(newVolume);
    }

    private void OnSoundVolumeSliderValueChanged(float newVolume)
    {
        AudioManager.Instance.SetSoundVolume(newVolume);
    }
}
