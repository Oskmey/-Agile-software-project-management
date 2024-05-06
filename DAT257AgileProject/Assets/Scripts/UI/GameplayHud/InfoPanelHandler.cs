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
        string trashName = trashType.ToReadableString();
        string article = ArticleForWordHelper.GetArticle(trashName);
        trashTypeText.text = $"You caught {article} {trashName}";
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
        string sourcesText;
        if (sources.Count == 1)
        {
            sourcesText = "Source: ";
        } 
        else
        {
            sourcesText = "Sources: ";
        }
        foreach (SourceData source in sources)
        {
            sourcesText += $"\n- {source.SourceName} ({source.Date}) <i>{source.Title}</i> {source.Website}";
        }
        trashSourcesText.text = sourcesText;
    }
}
