using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashScriptableObject", menuName = "ScriptableObjects/TrashScriptableObject", order = 1)]
public class TrashData : ScriptableObject
{
    [Tooltip("The type of the trash")]
    [SerializeField] 
    private TrashType trashType;

    [Tooltip("The category of the trash")]
    [SerializeField]
    private TrashCategory trashCategory;

    [Tooltip("The facts about the trash")]
    [SerializeField] 
    private List<TrashFactData> trashFacts;

    [Tooltip("The rarity of the trash")]
    [SerializeField] 
    private TrashRarity rarity;

    [Tooltip("The money value of the trash")]
    [SerializeField] 
    private int moneyValue;

    [Tooltip("Whether the trash is recyclable")]
    [SerializeField]
    private bool isRecyclable;

    public TrashType TrashType => trashType;
    public TrashCategory TrashCategory => trashCategory;
    public IReadOnlyList<TrashFactData> TrashFacts => trashFacts;
    public TrashRarity Rarity => rarity;
    public int MoneyValue => moneyValue;
    public bool IsRecyclable => isRecyclable;
}
