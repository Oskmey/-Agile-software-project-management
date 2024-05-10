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

    private MenuWithSettings otherMenuGameObject;

    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        // Finding the gameObject using a dedicated child game object. 
        // Since you can't have multiple tags on a single game object.
        otherMenuGameObject = FindMenuWithSettings();
        if (otherMenuGameObject == null)
        {
            Debug.LogError("otherMenuGameObject was null");
        }
    }

    private MenuWithSettings FindMenuWithSettings()
    {
        MenuWithSettings menuWithSettings = FindObjectOfType<MainMenu>(true);
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

    private void OnBackButtonClicked()
    {
        otherMenuGameObject.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

}
