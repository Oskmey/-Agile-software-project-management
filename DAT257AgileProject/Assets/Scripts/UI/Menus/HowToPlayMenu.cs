using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private readonly Dictionary<Button, HowToPlayScreenType> buttonToScreenTypeDictionary = new();

    private void Awake()
    {
        howToPlayScreenInformation = Resources.LoadAll<HowToPlayData>("ScriptableObjects");
        InitializeButtonToScreenTypeDictionary();
    }

    private void InitializeButtonToScreenTypeDictionary()
    {
        buttonToScreenTypeDictionary.Add(controlsButton, HowToPlayScreenType.Controls);
        buttonToScreenTypeDictionary.Add(trashFishingButton, HowToPlayScreenType.TrashFishing);
        buttonToScreenTypeDictionary.Add(shopButton, HowToPlayScreenType.Shop);
        buttonToScreenTypeDictionary.Add(inventoryButton, HowToPlayScreenType.Inventory);
        buttonToScreenTypeDictionary.Add(accessoriesButton, HowToPlayScreenType.Accessories);
        buttonToScreenTypeDictionary.Add(mapButton, HowToPlayScreenType.Map);
    }
}
