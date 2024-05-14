using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowToPlayMenu : MonoBehaviour
{
    [SerializeField]
    private Button controlsButton;
    [SerializeField]
    private Button trashFishingButton;
    [SerializeField]
    private Button shopButton;
    [SerializeField]
    private Button inventoryButton;
    [SerializeField]
    private Button accessoriesButton;
    [SerializeField]
    private Button mapButton;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private TextMeshProUGUI contentText;
    [SerializeField]
    private Image topImage;
    [SerializeField]
    private Image bottomImage;

    private HowToPlayData[] howToPlayScreenInformation;

    private void Awake()
    {
        howToPlayScreenInformation = Resources.LoadAll<HowToPlayData>("ScriptableObjects");
    }

    private void Start()
    {
        controlsButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Controls));
        trashFishingButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.TrashFishing));
        shopButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Shop));
        inventoryButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Inventory));
        accessoriesButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Accessories));
        mapButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Map));

        backButton.onClick.AddListener(() => OnBackButtonClicked());
    }

    private void OnButtonClicked(HowToPlayScreenType screenType)
    {
        HowToPlayData selectedData = FindData(screenType);

        ApplyDataToComponents(selectedData);
    }

    private HowToPlayData FindData(HowToPlayScreenType screenType)
    {
        return System.Array.Find(howToPlayScreenInformation, data => data.ScreenType == screenType);
    }

    private void ApplyDataToComponents(HowToPlayData selectedData)
    {
        if (selectedData != null)
        {
            contentText.text = selectedData.ContentText;
            if (selectedData.TopImage != null)
            {
                topImage.gameObject.SetActive(true);
                topImage.sprite = selectedData.TopImage;
            }
            else
            {
                topImage.gameObject.SetActive(false);
            }

            if (selectedData.BottomImage != null)
            {
                bottomImage.gameObject.SetActive(true);
                bottomImage.sprite = selectedData.BottomImage;
            }
            else
            {
                bottomImage.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Selected Data was null");
        }
    }

    private void OnBackButtonClicked()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
