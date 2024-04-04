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

    public void SetTrashNameText(TrashType trashType)
    {
        trashNameText.text = trashType.ToReadableString();
    }

    public void SetTrashDescriptionText(string text)
    {
        trashDescriptionText.text = text;
    }

    public void SetTrashMoneyValueText(int moneyValue)
    {
        trashMoneyValueText.text = $"Money: {moneyValue}";
    }

    public void SetTrashRarityText(TrashRarity trashRarity)
    {
        trashRarityText.text = $"Rarity: {trashRarity.ToReadableString()}";
    }
}
