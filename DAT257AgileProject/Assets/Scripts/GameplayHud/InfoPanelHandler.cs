using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanelHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI trashNameText;
    [SerializeField]
    private TextMeshProUGUI trashDescriptionText;
    [SerializeField]
    private TextMeshProUGUI trashMoneyValueText;
    [SerializeField]
    private TextMeshProUGUI trashRarityText;

    public void SetTrashNameText(string text)
    {
        trashNameText.text = text;
    }

    public void SetTrashDescriptionText(string text)
    {
        trashDescriptionText.text = text;
    }

    public void SetTrashMoneyValueText(string text)
    {
        trashMoneyValueText.text = $"Money: {text}";
    }

    public void SetTrashRarityText(string text)
    {
        trashRarityText.text = $"Rarity: {text}";
    }
}
