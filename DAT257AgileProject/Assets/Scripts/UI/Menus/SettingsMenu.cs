using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        InitializeUIComponents();

        otherMenuGameObject = FindMenuWithSettings();
        if (otherMenuGameObject == null)
        {
            Debug.LogError("otherMenuGameObject was null");
        }
    }

    private void InitializeUIComponents()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        InitializeMusicSlider();
        InitializeSoundSlider();
    }

    private void InitializeMusicSlider()
    {
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChanged);
        musicVolumeSlider.value = AudioManager.Instance.GetMusicVolume();
    }

    private void InitializeSoundSlider()
    {
        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderValueChanged);
        soundVolumeSlider.value = AudioManager.Instance.GetSoundVolume();
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

    private void OnMusicVolumeSliderValueChanged(float newVolume)
    {
        AudioManager.Instance.SetMusicVolume(newVolume);
    }

    private void OnSoundVolumeSliderValueChanged(float newVolume)
    {
        AudioManager.Instance.SetSoundVolume(newVolume);
    }
}
