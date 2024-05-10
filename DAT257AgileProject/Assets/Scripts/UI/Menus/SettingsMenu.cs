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

    private GameObject otherMenuGameObject;

    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        // Finding the gameObject using a dedicated child game object. 
        // Since you can't have multiple tags on a single game object.
        GameObject childGameObject = GameObject.FindGameObjectWithTag("ParentHasSettings");
        otherMenuGameObject = childGameObject.transform.parent.gameObject;
    }

    private void OnBackButtonClicked()
    {
        gameObject.SetActive(false);
        otherMenuGameObject.SetActive(true);
    }

}
