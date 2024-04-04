using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanelHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI trashTypeText;
    [SerializeField]
    private TextMeshProUGUI trashInformationText;
    [SerializeField]
    private TextMeshProUGUI trashMoneyValueText;
    [SerializeField]
    private TextMeshProUGUI trashRarityText;

    public void SetTrashTypeText(TrashType trashType)
    {
        trashTypeText.text = trashType.ToReadableString();
    }

    public void SetTrashInformationText(string trashInformation)
    {
        trashInformationText.text = trashInformation;
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
