using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashScriptableObject", menuName = "ScriptableObjects/TrashScriptableObject", order = 1)]
public class TrashData : ScriptableObject
{
    [SerializeField] 
    private TrashType trashType;
    [SerializeField] 
    private string trashInformation;
    [SerializeField] 
    private List<SourceData> sourcesInformation;
    [SerializeField] 
    private int rarity;
    [SerializeField] 
    private int moneyValue;

    public TrashType TrashType => trashType;
    public string TrashInformation => trashInformation;
    public IReadOnlyList<SourceData> SourcesInformation => sourcesInformation;
    public int Rarity => rarity;
    public int MoneyValue => moneyValue;
}
