using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WikiHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI factText;
    [SerializeField]
    private TextMeshProUGUI sourceInfoText;

    public void SetFactText(string factInfo)
    {
        factText.text = $"{factInfo}";
    }

    public void SetSourceText(IReadOnlyList<SourceData> sources)
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
        sourceInfoText.text = sourcesText;
    }
}
