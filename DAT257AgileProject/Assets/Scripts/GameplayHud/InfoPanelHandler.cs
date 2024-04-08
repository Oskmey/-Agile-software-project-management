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
    [SerializeField]
    private TextMeshProUGUI trashSourcesText;

    public void SetTrashTypeText(TrashType trashType)
    {
        trashTypeText.text = $"You caught a {trashType.ToReadableString()}";
    }

    public void SetTrashInformationText(string trashInformation)
    {
        trashInformationText.text = $"Did you know {trashInformation}";
    }

    public void SetTrashMoneyValueText(int moneyValue)
    {
        trashMoneyValueText.text = $"Money: {moneyValue}";
    }

    public void SetTrashRarityText(TrashRarity trashRarity)
    {
        trashRarityText.text = $"Rarity: {trashRarity.ToReadableString()}";
    }

    public void SetTrashSourcesText(IReadOnlyList<SourceData> sources)
    {
        string sourcesText = "Sources: ";
        foreach (SourceData source in sources)
        {
            // ToShortDateString() is used to only show the date without the time
            sourcesText += $"\n- {source.SourceName} ({source.Date.ToShortDateString()}) <i>{source.Title}</i> {source.Website}";
        }
        trashSourcesText.text = sourcesText;
    }
}
