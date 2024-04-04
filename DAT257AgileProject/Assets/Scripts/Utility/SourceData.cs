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
    [SerializeField]
    private string title;
    [SerializeField]
    private string link;

    public DateTime Date
    {
        get { return DateTime.Parse(dateAsString); }
    }

    public string SourceName => sourceName;
    public string Title => title;
    public string Link => link;
}
