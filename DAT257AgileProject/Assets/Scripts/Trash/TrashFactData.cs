using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashFactScriptableObject", menuName = "ScriptableObjects/TrashFactScriptableObject", order = 3)]
public class TrashFactData : ScriptableObject
{
    [TextArea(1, 10)]
    [Tooltip("Fact about the trash")]
    [SerializeField] 
    private string trashFact;

    [Tooltip("Sources of the trash fact, the sources are scriptable objects")]
    [SerializeField]
    private List<SourceData> sourcesInformation;

    public string TrashFact => trashFact;
    public IReadOnlyList<SourceData> SourcesInformation => sourcesInformation;
}
