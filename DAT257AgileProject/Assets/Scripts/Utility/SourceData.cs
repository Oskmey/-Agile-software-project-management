using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSourceScriptableObject", menuName = "ScriptableObjects/SourceScriptableObject", order = 2)]
public class SourceData : ScriptableObject
{
    [SerializeField]
    private string sourceName;
    // Needs to be a string because DateTime doesn't show up in the inspector
    [SerializeField]
    private string dateAsString;
    [TextArea(1, 10)]
    [SerializeField]
    private string title;
    [SerializeField]
    private string website;
    [TextArea(1, 10)]
    [SerializeField]
    private string link;
    [SerializeField]
    private string retrievalDateAsString;

    public DateTime Date
    {
        get { return DateTime.Parse(dateAsString); }
    }

    public DateTime RetrievalDate
    {
        get { return DateTime.Parse(retrievalDateAsString); }
    }

    public string SourceName => sourceName;
    public string Title => title;
    public string Website => website;
    public string Link => link;
}
