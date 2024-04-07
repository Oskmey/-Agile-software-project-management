using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashFactScriptableObject", menuName = "ScriptableObjects/TrashFactScriptableObject", order = 3)]
public class TrashFactData : ScriptableObject
{
    [TextArea(1, 10)]
    [SerializeField] 
    private string trashFact;
    [SerializeField]
    private List<SourceData> sourcesInformation;

    public string TrashFact => trashFact;
    public IReadOnlyList<SourceData> SourcesInformation => sourcesInformation;
}
