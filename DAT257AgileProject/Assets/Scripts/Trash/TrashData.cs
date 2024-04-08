using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashScriptableObject", menuName = "ScriptableObjects/TrashScriptableObject", order = 1)]
public class TrashData : ScriptableObject
{
    [SerializeField] 
    private TrashType trashType;
    [SerializeField]
    private TrashCategory trashCategory;
    [SerializeField] 
    private List<TrashFactData> trashFacts;
    [SerializeField] 
    private TrashRarity rarity;
    [SerializeField] 
    private int moneyValue;
    [SerializeField]
    private bool isRecyclable;

    public TrashType TrashType => trashType;
    public TrashCategory TrashCategory => trashCategory;
    public IReadOnlyList<TrashFactData> TrashFacts => trashFacts;
    public TrashRarity Rarity => rarity;
    public int MoneyValue => moneyValue;
    public bool IsRecyclable => isRecyclable;
}
