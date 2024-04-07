using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashScriptableObject", menuName = "ScriptableObjects/TrashScriptableObject", order = 1)]
public class TrashData : ScriptableObject
{
    [SerializeField] 
    private TrashType trashType;
    [SerializeField] 
    private List<TrashFactData> trashFacts;
    [SerializeField] 
    private TrashRarity rarity;
    [SerializeField] 
    private int moneyValue;

    public TrashType TrashType => trashType;
    public IReadOnlyList<TrashFactData> TrashFacts => trashFacts;
    public TrashRarity Rarity => rarity;
    public int MoneyValue => moneyValue;
}
