using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private static readonly HowToPlayScreenType defaultScreenType = HowToPlayScreenType.Controls;
    private IMenuWithHowToPlay otherMenuGameObject;

    private PlayerInputActions playerInputActions;
    private InputAction BackAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        howToPlayScreenInformation = Resources.LoadAll<HowToPlayData>("ScriptableObjects");
    }

    private void Start()
    {
        InitializeButtonListeners();

        SetHowToPlayScreenToDefualt();

        otherMenuGameObject = FindMenuWithHowToPlay();
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

    private void InitializeButtonListeners()
    {
        controlsButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Controls));
        trashFishingButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.TrashFishing));
        shopButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Shop));
        inventoryButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Inventory));
        accessoriesButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Accessories));
        mapButton.onClick.AddListener(() => OnButtonClicked(HowToPlayScreenType.Map));

        backButton.onClick.AddListener(() => OnBackButtonClicked());
    }

    private void SetHowToPlayScreenToDefualt()
    {
        HowToPlayData defaultScreen = FindData(defaultScreenType);
        ApplyDataToUIComponents(defaultScreen);
    }

    private IMenuWithHowToPlay FindMenuWithHowToPlay()
    {
        IMenuWithHowToPlay menuWithHowToPlay = FindObjectOfType<MainMenu>(true);
        if (menuWithHowToPlay != null)
        {
            return menuWithHowToPlay;
        }

        Debug.LogError("Could not find menu with how to play");
        return null;
    }

    private void OnButtonClicked(HowToPlayScreenType screenType)
    {
        HowToPlayData selectedData = FindData(screenType);

        ApplyDataToUIComponents(selectedData);
    }

    private HowToPlayData FindData(HowToPlayScreenType screenType)
    {
        return System.Array.Find(howToPlayScreenInformation, data => data.ScreenType == screenType);
    }

    private void ApplyDataToUIComponents(HowToPlayData selectedData)
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
        otherMenuGameObject.GetGameObject().SetActive(true);
        gameObject.SetActive(false);
    }
}
