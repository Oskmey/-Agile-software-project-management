using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    [SerializeField] 
    private TrashData trashScriptableObject;

    // Attributes set via the scriptable object
    private TrashType trashType;
    private List<TrashCategory> trashCategories;
    private List<TrashFactData> trashFacts;
    private TrashRarity rarity;
    private int moneyValue;
    private bool isRecyclable;

    // Awake and not Start for it to set the attributes
    // when the object is created
    private void Awake()
    {
        LoadAttributesFromScriptableObject();
    }

    private void LoadAttributesFromScriptableObject()
    {
        trashType = trashScriptableObject.TrashType;
        trashCategories = new List<TrashCategory>(trashScriptableObject.TrashCategories);
        trashFacts = new List<TrashFactData>(trashScriptableObject.TrashFacts);
        rarity = trashScriptableObject.Rarity;
        moneyValue = trashScriptableObject.MoneyValue;
        isRecyclable = trashScriptableObject.IsRecyclable;
    }

    public TrashFactData GetRandomTrashFact()
    {
        int randomInteger = UnityEngine.Random.Range(0, trashFacts.Count);
        TrashFactData randomTrashFact = trashFacts[randomInteger];
        return randomTrashFact;
    }

    public TrashType TrashType => trashType;
    public IReadOnlyList<TrashCategory> TrashCategories => trashCategories;
    public IReadOnlyList<TrashFactData> TrashFacts => trashFacts;
    public TrashRarity Rarity => rarity;
    public int MoneyValue => moneyValue;
    public bool IsRecyclable => isRecyclable;
}
